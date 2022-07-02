using System;
using System.Collections.Generic;
using System.Linq;

namespace ThirtyDollarWebsiteConverter
{
    public static class LongThings
    {
        public static readonly List<string> AudioFiles = new()
        {
            "boom",
            "bruh",
            "bong",
            "💀",
            "👏",
            "🐶",
            "👽",
            "🔔",
            "💢",
            "💨",
            "🚫",
            "📲",
            "🌄",
            "🏏",
            "🤬",
            "🚨",
            "buzzer",
            "❗",
            "🦀",
            "e",
            "eight",
            "🍕",
            "🦢",
            "gun",
            "hitmarker",
            "👌",
            "whatsapp",
            "gnome",
            "💿",
            "🎉",
            "🎻",
            "pan",
            "slip",
            "whipcrack",
            "explosion",
            "oof",
            "subaluwa",
            "necoarc",
            "yoda",
            "hehehehaw",
            "granddad",
            "morshu",
            "stopposting",
            "21",
            "op",
            "SLAM",
            "americano",
            "smw_coin",
            "smw_1up",
            "smw_spinjump",
            "smw_stomp2",
            "smw_kick",
            "smw_stomp",
            "yahoo",
            "sm64_hurt",
            "bup",
            "thwomp",
            "sm64_painting",
            "smm_scream",
            "mariopaint_mario",
            "mariopaint_luigi",
            "smw_yoshi",
            "mariopaint_star",
            "mariopaint_flower",
            "mariopaint_gameboy",
            "mariopaint_dog",
            "mariopaint_cat",
            "mariopaint_swan",
            "mariopaint_baby",
            "mariopaint_plane",
            "mariopaint_car",
            "shaker",
            "🥁",
            "hammer",
            "🪘",
            "sidestick",
            "ride2",
            "buttonpop",
            "skipshot",
            "otto_on",
            "otto_off",
            "otto_happy",
            "otto_stress",
            "tab_sounds",
            "tab_rows",
            "tab_actions",
            "tab_decorations",
            "tab_rooms",
            "preecho",
            "tonk",
            "rdmistake",
            "samurai",
            "adofaikick",
            "midspin",
            "adofaicymbal",
            "cowbell",
            "karateman_throw",
            "karateman_offbeat",
            "karateman_hit",
            "karateman_bulb",
            "ook",
            "choruskid",
            "builttoscale",
            "perfectfail",
            "🌟",
            "hoenn",
            "🎺",
            "fnf_left",
            "fnf_down",
            "fnf_up",
            "fnf_right",
            "fnf_death",
            "megalovania",
            "🦴",
            "undertale_encounter",
            "undertale_hit",
            "undertale_crack",
            "toby",
            "gaster",
            "gdcrash",
            "gdcrash_orbs",
            "gd_coin",
            "gd_orbs",
            "gd_diamonds",
            "bwomp",
            "isaac_hurt",
            "isaac_dead",
            "isaac_mantle",
            "terraria_star",
            "terraria_pot",
            "terraria_reforge",
            "BABA",
            "YOU",
            "DEFEAT",
            "celeste_dash",
            "celeste_death",
            "celeste_spring",
            "celeste_diamond",
            "ultrainstinct",
            "flipnote",
            "amongus",
            "amongdrip",
            "amogus",
            "noteblock_harp",
            "noteblock_bass",
            "noteblock_snare",
            "noteblock_click",
            "noteblock_bell",
            "noteblock_chime",
            "noteblock_banjo",
            "noteblock_pling",
            "noteblock_xylophone",
            "noteblock_bit",
            "minecraft_explosion",
            "minecraft_bell",
            "last",
            "last",
            "last",
            "last",
            "last",
            "last",
            "last",
            "last",
            "last"
        };

        private static string GetFilename(this SoundEvent ev)
        {
            return ev switch
            {
                SoundEvent.Combine => "!combine",
                SoundEvent.Pause => "_pause",
                SoundEvent.VineBoom => "boom",
                SoundEvent.BruhSoundEffectNumber2 => "bruh",
                SoundEvent.Bong => "bong",
                SoundEvent.Skeleton => "💀",
                SoundEvent.ReverbClap => "👏",
                SoundEvent.WhatTheDogDoin => "🐶",
                SoundEvent.CaveNoise => "👽",
                SoundEvent.Bell => "🔔",
                SoundEvent.Boink => "💢",
                SoundEvent.ReverbFart => "💨",
                SoundEvent.Error => "🚫",
                SoundEvent.SkylineNotification => "📲",
                SoundEvent.MorningFlower => "🌄",
                SoundEvent.Bonk => "🏏",
                SoundEvent.Bleep => "🤬",
                SoundEvent.Alarm => "🚨",
                SoundEvent.IncorrectBuzzer => "buzzer",
                SoundEvent.Alert => "❗",
                SoundEvent.MrKrabsWalking => "🦀",
                SoundEvent.E => "e",
                SoundEvent.Number8 => "eight",
                SoundEvent.AyoThePizzaHere => "🍕",
                SoundEvent.Honk => "🦢",
                SoundEvent.GunReload => "gun",
                SoundEvent.Hitmarker => "hitmarker",
                SoundEvent.NoiceClick => "👌",
                SoundEvent.WhistleNotification => "whatsapp",
                SoundEvent.Gnome => "gnome",
                SoundEvent.RecordScratch => "💿",
                SoundEvent.Tada => "🎉",
                SoundEvent.Waterphone => "🎻",
                SoundEvent.FryingPan => "pan",
                SoundEvent.Slip => "slip",
                SoundEvent.WhipCrack => "whipcrack",
                SoundEvent.Explosion => "explosion",
                SoundEvent.Oof => "oof",
                SoundEvent.Subaluwa => "subaluwa",
                SoundEvent.NecoArc => "necoarc",
                SoundEvent.LegoYodaDeath => "yoda",
                SoundEvent.HeHeHeHaw => "hehehehaw",
                SoundEvent.GrandDad => "granddad",
                SoundEvent.MorshuMmm => "morshu",
                SoundEvent.Ding => "stopposting",
                SoundEvent.TwentyOne => "21",
                SoundEvent.Op => "op",
                SoundEvent.Slam => "SLAM",
                SoundEvent.DogDancingWeNoSpeakAmericano => "americano",
                SoundEvent.Coin => "smw_coin",
                SoundEvent.OneUp => "smw_1up",
                SoundEvent.SpinJump => "smw_spinjump",
                SoundEvent.Stomp => "smw_stomp2",
                SoundEvent.KickSuperMarioWorld => "smw_kick",
                SoundEvent.BossStomp => "smw_stomp",
                SoundEvent.LongJump => "yahoo",
                SoundEvent.Damage => "sm64_hurt",
                SoundEvent.Bup => "bup",
                SoundEvent.Thwomp => "thwomp",
                SoundEvent.Painting => "sm64_painting",
                SoundEvent.Scream => "smm_scream",
                SoundEvent.Mario => "mariopaint_mario",
                SoundEvent.SteelDrum => "mariopaint_luigi",
                SoundEvent.Yoshi => "smw_yoshi",
                SoundEvent.Star => "mariopaint_star",
                SoundEvent.Flower => "mariopaint_flower",
                SoundEvent.Gameboy => "mariopaint_gameboy",
                SoundEvent.Dog => "mariopaint_dog",
                SoundEvent.Cat => "mariopaint_cat",
                SoundEvent.Swan => "mariopaint_swan",
                SoundEvent.Baby => "mariopaint_baby",
                SoundEvent.Plane => "mariopaint_plane",
                SoundEvent.Car => "mariopaint_car",
                SoundEvent.Shaker => "shaker",
                SoundEvent.KickRhythmDoctor => "🥁",
                SoundEvent.Hammer => "hammer",
                SoundEvent.TomDrum => "🪘",
                SoundEvent.Sidestick => "sidestick",
                SoundEvent.Ride => "ride2",
                SoundEvent.Pop => "buttonpop",
                SoundEvent.Skipshot => "skipshot",
                SoundEvent.OttoEnabled => "otto_on",
                SoundEvent.OttoDisabled => "otto_off",
                SoundEvent.OttoHappy => "otto_happy",
                SoundEvent.OttoStressed => "otto_stress",
                SoundEvent.SoundsTab => "tab_sounds",
                SoundEvent.RowsTab => "tab_rows",
                SoundEvent.ActionsTab => "tab_actions",
                SoundEvent.DecorationsTab => "tab_decorations",
                SoundEvent.RoomsTab => "tab_rooms",
                SoundEvent.PreEchoClap => "preecho",
                SoundEvent.Tonk => "tonk",
                SoundEvent.Mistake => "rdmistake",
                SoundEvent.SamuraiTechno => "samurai",
                SoundEvent.KickADanceOfFireAndIce => "adofaikick",
                SoundEvent.MidspinClack => "midspin",
                SoundEvent.LevelComplete => "adofaicymbal",
                SoundEvent.Cowbell => "cowbell",
                SoundEvent.KarateManToss => "karateman_throw",
                SoundEvent.KarateManOffbeatToss => "karateman_offbeat",
                SoundEvent.KarateManHit => "karateman_hit",
                SoundEvent.KarateManBulb => "karateman_bulb",
                SoundEvent.Ook => "ook",
                SoundEvent.ChorusKid => "choruskid",
                SoundEvent.Widget => "builttoscale",
                SoundEvent.PerfectFailed => "perfectfail",
                SoundEvent.SkillStar => "🌟",
                SoundEvent.HoennTrumpet => "hoenn",
                SoundEvent.ZunPet => "🎺",
                SoundEvent.Left => "fnf_left",
                SoundEvent.Down => "fnf_down",
                SoundEvent.Up => "fnf_up",
                SoundEvent.Right => "fnf_right",
                SoundEvent.Fail => "fnf_death",
                SoundEvent.Megalovania => "megalovania",
                SoundEvent.MegalovaniaNote => "🦴",
                SoundEvent.Encounter => "undertale_encounter",
                SoundEvent.DamageUndertale => "undertale_hit",
                SoundEvent.GameOver => "undertale_crack",
                SoundEvent.Bark => "toby",
                SoundEvent.Vanish => "gaster",
                SoundEvent.Crash => "gdcrash",
                SoundEvent.CrashOrbs => "gdcrash_orbs",
                SoundEvent.SecretCoin => "gd_coin",
                SoundEvent.ManaOrbs => "gd_orbs",
                SoundEvent.Diamond => "gd_diamonds",
                SoundEvent.BlastProcessing => "bwomp",
                SoundEvent.DamageTheBindingOfIsaac => "isaac_hurt",
                SoundEvent.DeathTheBindingOfIsaac => "isaac_dead",
                SoundEvent.HolyMantle => "isaac_mantle",
                SoundEvent.Magic => "terraria_star",
                SoundEvent.Shatter => "terraria_pot",
                SoundEvent.Reforge => "terraria_reforge",
                SoundEvent.Move => "BABA",
                SoundEvent.Rule => "YOU",
                SoundEvent.DefeatBabaIsYou => "DEFEAT",
                SoundEvent.Dash => "celeste_dash",
                SoundEvent.DeathCeleste => "celeste_death",
                SoundEvent.Spring => "celeste_spring",
                SoundEvent.DashGem => "celeste_diamond",
                SoundEvent.UltraInstinct => "ultrainstinct",
                SoundEvent.Frog => "flipnote",
                SoundEvent.AmongUs => "amongus",
                SoundEvent.AmongDrip => "amongdrip",
                SoundEvent.Amogus => "amogus",
                SoundEvent.NoteBlockHarp => "noteblock_harp",
                SoundEvent.NoteBlockBass => "noteblock_bass",
                SoundEvent.NoteBlockSnare => "noteblock_snare",
                SoundEvent.NoteBlockHat => "noteblock_click",
                SoundEvent.NoteBlockBell => "noteblock_bell",
                SoundEvent.NoteBlockChime => "noteblock_chime",
                SoundEvent.NoteBlockBanjo => "noteblock_banjo",
                SoundEvent.NoteBlockPling => "noteblock_pling",
                SoundEvent.NoteBlockXylophone => "noteblock_xylophone",
                SoundEvent.NoteBlockBit => "noteblock_bit",
                SoundEvent.ExplosionMinecraft => "minecraft_explosion",
                SoundEvent.BellMinecraft => "minecraft_bell",
                SoundEvent.Speed => "expr",
                SoundEvent.GoToLoop => "expr",
                SoundEvent.LoopTarget => "expr",
                SoundEvent.CutAllSounds => "expr",
                SoundEvent.JumpToTarget => "expr",
                SoundEvent.SetTarget => "expr",
                SoundEvent.None => "none",
                SoundEvent.Volume => "volume",
                _ => throw new ArgumentOutOfRangeException(nameof(ev), ev, null)
            };
        }

        public static string ListElements(this IEnumerable<Event> events)
        {
            return string.Join(" ", events.Select(e => e.SoundEvent?.GetFilename()));
        }
    }
}