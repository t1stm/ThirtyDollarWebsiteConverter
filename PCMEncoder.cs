using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ThirtyDollarWebsiteConverter
{
    public class PcmEncoder
    {
        private const uint SampleRate = 48000; //Hz
        private const int Channels = 1;
        private List<float> PcmBytes { get; } = new();
        public Composition? Composition { get; init; }

        private void AddOrChangeByte(float pcmByte, int index)
        {
            lock (PcmBytes)
            {
                if (index < PcmBytes.Count)
                {
                    PcmBytes[index] = MixSamples(pcmByte, PcmBytes[index]);
                    return;
                }

                if (index >= PcmBytes.Count) FillWithZeros(index);
                PcmBytes[index] = pcmByte;
            }
        }
        

        private float MixSamples(float sampleOne, float sampleTwo)
        {
            return sampleOne + sampleTwo;
        }

        private void FillWithZeros(int index)
        {
            lock (PcmBytes)
            {
                while (index >= PcmBytes.Count) PcmBytes.Add(0);
            }
        }

        private void CalculateVolume()
        {
            if (Composition == null) throw new Exception("Null Composition");
            double volume = 100;
            lock (Composition.Events)
            {
                foreach (var ev in Composition.Events) //Quick pass for volume
                {
                    switch (ev.SoundEvent)
                    {
                        case SoundEvent.Volume:
                            switch (ev.ValueScale)
                            {
                                case ValueScale.Times:
                                    volume *= ev.Value;
                                    break;
                                case ValueScale.Add:
                                    volume += ev.Value;
                                    break;
                                case ValueScale.None:
                                    volume = ev.Value;
                                    break;
                            }
                            break;
                    }
                    ev.Volume = volume;
                }
                Composition.Events.RemoveAll(e => e.SoundEvent is SoundEvent.Volume);
            }
        }
        
        public void Start()
        {
            if (Composition == null) throw new Exception("Null Composition");
            var bpm = 300.0;
            var position = (ulong) (SampleRate / (bpm / 60));
            var count = Composition.Events.Count;
            CalculateVolume();
            
            for (var i = 0; i < Composition!.Events.Count; i++)
            {
                var ev = Composition.Events[i];
                try
                {
                    switch (ev.SoundEvent)
                    {
                        case SoundEvent.Speed:
                            switch (ev.ValueScale)
                            {
                                case ValueScale.Times:
                                    bpm *= ev.Value;
                                    break;
                                case ValueScale.Add:
                                    bpm += ev.Value;
                                    break;
                                case ValueScale.None:
                                    bpm = ev.Value;
                                    break;
                            }
                            count--;
                            Console.WriteLine($"BPM is now: {bpm}");
                            continue;

                        case SoundEvent.GoToLoop:
                            count--;
                            if (ev.Loop <= 0) continue;
                            ev.Loop--;
                            for (var j = i; j > 0; j--)
                            {
                                if (Composition.Events[j].SoundEvent != SoundEvent.LoopTarget)
                                {
                                    continue;
                                }
                                i = j - 1;
                                break; // Ooga booga, I am retarded. I've been debugging this for loop for two hours now. How could've I forgotten to add the break?
                            }

                            Console.WriteLine($"Going to element: ({i + 1}) - \"{Composition.Events[i + 1]}\"");
                            continue;

                        case SoundEvent.JumpToTarget:
                            if (ev.Loop <= 0) continue;
                            ev.Loop--;
                            //i = Triggers[(int) ev.Value - 1] - 1;
                            var item = Composition.Events.FirstOrDefault(r =>
                                r.SoundEvent == SoundEvent.SetTarget && (int) r.Value == (int) ev.Value);
                            if (item == null)
                            {
                                Console.WriteLine($"Unable to target with id: {ev.Value}");
                                continue;
                            }
                            i = Composition.Events.IndexOf(item) - 1;
                            Console.WriteLine($"Jumping to element: ({i}) - {Composition.Events[i]}");
                            count--;
                            //
                            continue;

                        case SoundEvent.Pause:
                            Console.WriteLine($"Pausing for: {ev.Loop} beats.");
                            var oldLoop = ev.Loop;
                            while (ev.Loop >= 1)
                            {
                                ev.Loop--;
                                position += (ulong) (SampleRate / (bpm / 60));
                            }
                            count--;
                            ev.Loop = oldLoop;
                            continue;
                        
                        case SoundEvent.CutAllSounds or SoundEvent.None or SoundEvent.LoopTarget or SoundEvent.SetTarget or SoundEvent.Volume:
                            count--;
                            continue;
                        
                        case SoundEvent.Combine:
                            position -= (ulong) (SampleRate / (bpm / 60));
                            continue;
                        
                        default: 
                            position += (ulong) (SampleRate / (bpm / 60));
                            break;
                    }

                    var breakEarly = i + count < Composition?.Events.Count &&
                                     Composition?.Events[i + 1].SoundEvent == SoundEvent.CutAllSounds;
                    var index = (int) (position - position % 2);
                    Console.WriteLine($"Processing Event: [{index}] - \"{ev}\"");
                    HandleProcessing(ev, index, breakEarly ? (int) (SampleRate / (bpm / 60)) : -1);
                    if (ev.Loop > 1)
                    {
                        ev.Loop--;
                        i--;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        private class ProcessedSample
        {
            public short[]? SampleData { get; init; }
            public int ProcessedChunks { get; set; }
            public int SampleLength => SampleData?.Length ?? 0;
            public double Volume { get; init; }
        }

        private void HandleProcessing(Event ev, int index, int breakAtIndex)
        {
            try
            {
                ProcessedSample sample = ev.Value == 0 ? new ProcessedSample
                {
                    SampleData = Program.Samples[ev.SampleId],
                    Volume = ev.Volume
                } : new ProcessedSample
                {
                    SampleData = Resample(Program.Samples[ev.SampleId], SampleRate, (uint) (SampleRate / Math.Pow(2, ev.Value / 12)), Channels),
                    Volume = ev.Volume
                };

                var size = breakAtIndex == -1 ? sample.SampleLength : breakAtIndex;
                for (sample.ProcessedChunks = 0; sample.ProcessedChunks < size; sample.ProcessedChunks++)
                {
                    if (breakAtIndex != -1 && sample.ProcessedChunks >= breakAtIndex) return;
                    AddOrChangeByte((float) ((sample.SampleData?[sample.ProcessedChunks] ?? 0) * (sample.Volume * 0.5 / 100)) / 32768f, index + sample.ProcessedChunks);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Processing failed: \"{e}\"");
            }
        }
        
        private unsafe float[] Resample(float[] samples, uint sampleRate, uint targetSampleRate, uint channels)
        {
            fixed (float* vals = samples)
            {
                var length = Resample32BitFloat(vals, null, sampleRate, targetSampleRate, (ulong) samples.LongLength,
                    channels);
                float[] alloc = new float[length];
                fixed (float* output = alloc)
                {
                    Resample32BitFloat(vals, output, sampleRate, targetSampleRate, (ulong) samples.LongLength, channels);
                }

                return alloc;
            }
        }

        private unsafe short[] Resample(short[] samples, uint sampleRate, uint targetSampleRate, uint channels)
        {
            fixed (short* vals = samples)
            {
                var length = Resample16Bit(vals, null, sampleRate, targetSampleRate, (ulong) samples.LongLength,
                    channels);
                short[] alloc = new short[length];
                fixed (short* output = alloc)
                {
                    Resample16Bit(vals, output, sampleRate, targetSampleRate, (ulong) samples.LongLength, channels);
                }

                return alloc;
            }
        }

        // Original Source: https://github.com/cpuimage/resampler
        
        private unsafe ulong Resample32BitFloat(float *input, float* output, uint inSampleRate, uint outSampleRate, ulong inputSize, uint channels) {
            
            if (input == null)
                return 0;
            var outputSize = (ulong) (inputSize * (double) outSampleRate / inSampleRate);
            outputSize -= outputSize % channels;
            if (output == null)
                return outputSize;
            var stepDist = inSampleRate / (double) outSampleRate;
            const ulong fixedFraction = (ulong) 1 << 32;
            const double normFixed = 1.0 / ((ulong) 1 << 32);
            var step = (ulong) (stepDist * fixedFraction + 0.5);
            ulong curOffset = 0;
            for (uint i = 0; i < outputSize; i += 1) {
                for (uint c = 0; c < channels; c += 1) {
                    *output++ = (float) (input[c] + (input[c + channels] - input[c]) * (
                                (curOffset >> 32) + (curOffset & (fixedFraction - 1)) * normFixed
                            )
                        );
                }
                curOffset += step;
                input += (curOffset >> 32) * channels;
                curOffset &= fixedFraction - 1;
            }
            return outputSize;
            
        }
        
        private unsafe ulong Resample16Bit(short* input, short* output, uint inSampleRate, uint outSampleRate,
            ulong inputSize, uint channels)
        {
            var outputSize = (ulong) (inputSize * (double) outSampleRate / inSampleRate);
            outputSize -= outputSize % channels;
            if (output == null)
                return outputSize;
            var stepDist = (double) inSampleRate / outSampleRate;
            const ulong fixedFraction = (ulong) 1 << 32;
            const double normFixed = 1.0 / ((ulong) 1 << 32);
            var step = (ulong) (stepDist * fixedFraction + 0.5);
            ulong curOffset = 0;
            for (uint i = 0; i < outputSize; i += 1)
            {
                for (uint c = 0; c < channels; c += 1)
                    *output++ = (short) (input[c] + (input[c + channels] - input[c]) *
                        ((curOffset >> 32) + (curOffset & (fixedFraction - 1)) * normFixed));
                curOffset += step;
                input += (curOffset >> 32) * channels;
                curOffset &= fixedFraction - 1;
            }

            return outputSize;
        }

        public void Play(int num)
        {
            PcmBytes.NormalizeVolume();
            var stream = new BinaryWriter(File.Open($"./out-{num}.wav", FileMode.Create));
            AddWavHeader(stream);
            stream.Write((short) 0);
            foreach (var data in PcmBytes) stream.Write((short) (data * 32768));
            stream.Close();
        }

        private void AddWavHeader(BinaryWriter writer)
        {
            writer.Write(new[]{'R','I','F','F'}); // RIFF Chunk Descriptor
            writer.Write(4 + 8 + 16 + 8 + PcmBytes.Count * 2); // Sub Chunk 1 Size
            //Chunk Size 4 bytes.
            writer.Write(new[]{'W','A','V','E'});
            // fmt sub-chunk
            writer.Write(new[]{'f','m','t',' '});
            writer.Write(16); // Sub Chunk 1 Size
            writer.Write((short) 1); // Audio Format 1 = PCM
            writer.Write((short) Channels); // Audio Channels
            writer.Write(SampleRate); // Sample Rate
            writer.Write(SampleRate * Channels * 2 /* Bytes */); // Byte Rate
            writer.Write((short) (Channels * 2)); // Block Align
            writer.Write((short) 16); // Bits per Sample
            // data sub-chunk
            writer.Write(new[]{'d','a','t','a'});
            writer.Write(PcmBytes.Count * Channels * 2); // Sub Chunk 2 Size.
            
        }
    }
}