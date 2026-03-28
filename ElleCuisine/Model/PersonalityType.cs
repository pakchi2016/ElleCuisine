using MaidensRecipe.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaidensRecipe.Models
{
    // 乙女の理性を形作る12の性格タイプ
    public enum PersonalityType
    {
        Pure,       // 清楚 (王道には強いが、不浄・排泄器官に脆い)
        Defiant,    // 強気 (正面からの快楽には耐えるが、屈辱や内部侵入に弱い)
        Timid,      // 弱気 (防壁は削れやすいが、快楽自体は溜まりにくい)
        Emotional,  // 感情的 (快楽の蓄積が早いが、スタミナ消費も激しい)
        Apathetic,  // 無感動 (表面の愛撫を無視し、最深部の刺激にのみ反応)
        Arrogant,   // 傲慢 (プライドが高く、尊厳をへし折る陵辱に極端に弱い)
        Fastidious, // 潔癖 (体液や粘液などの汚染系に致命的に弱い)
        Bluffing,   // 虚勢 (禁欲度は高いが、特定部位の個別倍率が異常に高い)
        Devoted,    // 献身 (精神防壁が異常に硬いが、一度落ちると依存しやすい)
        Stubborn,   // 頑固 (新しい刺激には強いが、同じ部位の反復攻撃に弱い)
        Hedonistic, // 享楽的 (禁欲度が低くすぐ受け入れるが、飽きっぽい)
        Cowardly    // 臆病 (肉体快楽より、恐怖や羞恥の精神攻撃で自滅する)
    }

    // 先天的な興奮特殊バフ（特定の状況で発動する性癖トリガー）
    public enum ExcitementSpecialTrait
    {
        None,               // 特殊な性癖なし（標準）
        Masochist,          // マゾヒスト（苦痛や強力な拘束を伴う攻撃で興奮UP）
        ShameLover,         // 羞恥嗜好（尊厳が削られる屈辱的な状況で興奮UP）
        SubmissionDesire,   // 被支配欲（スタミナが半分以下の絶望的状況で興奮UP）
        ImmoralPleasure     // 背徳の悦び（禁欲度に反する「弱点部位」を責められた時に興奮UP）
    }

    public class PersonalityData
    {
        public double Mouth { get; set; } = 1.0;
        public double Urethra { get; set; } = 1.0;
        public double Breasts { get; set; } = 1.0;
        public double Anus { get; set; } = 1.0;
        public double Clitoris { get; set; } = 1.0;
        public double Armpits { get; set; } = 1.0;
        public double Vagina { get; set; } = 1.0;
        public double Navel { get; set; } = 1.0;
        public double Womb { get; set; } = 1.0;
        public double Feet { get; set; } = 1.0;
        public double BaseExcitement { get; set; } = 1.0;
        public ExcitementSpecialTrait SpecialTrait { get; set; } = ExcitementSpecialTrait.None;
        public string personalityText { get; set; } = "その内面はまだ読み切れない…";

        public PersonalityData(MaidenJob job)
        {
            // 性格に応じて特定の部位の数値を上書き（個性を付与）します
            switch (job.jobData.Personality)
            {
                case PersonalityType.Pure: // 1. 清楚
                    Breasts = 0.5;
                    Clitoris = 0.5;
                    Mouth = 1.5;
                    Anus = 1.8;
                    Womb = 2.0;
                    BaseExcitement = 0.8; // 自己抑制
                    SpecialTrait = ExcitementSpecialTrait.ImmoralPleasure;
                    personalityText = "無垢な瞳がこちらを見つめている。" ;
                    break;

                case PersonalityType.Defiant: // 2. 強気
                    Breasts = 0.7;
                    Clitoris = 0.7;
                    Vagina = 1.2;
                    Anus = 1.8;
                    BaseExcitement = 0.8; // 自己抑制
                    SpecialTrait = ExcitementSpecialTrait.Masochist; // 拘束と苦痛に抗うほど熱を帯びる
                    personalityText = "怯むどころかこちらを睨みつけている";
                    break;

                case PersonalityType.Timid: // 3. 弱気
                    Vagina = 0.7;
                    Anus = 0.7;
                    Breasts = 1.2;
                    Clitoris = 1.2;
                    SpecialTrait = ExcitementSpecialTrait.SubmissionDesire;// 圧倒的な力に屈服する悦び
                    personalityText = "小動物のようにただ肩を震わせている。";
                    break;

                case PersonalityType.Emotional: // 4. 感情的
                    Mouth = 1.2;
                    Breasts = 1.2;
                    Clitoris = 1.5;
                    Vagina = 1.5;
                    Womb = 1.5;
                    BaseExcitement = 1.5; // 生来の好色（場の空気にすぐ呑まれる）
                    SpecialTrait = ExcitementSpecialTrait.None;
                    personalityText = "恐怖と怒りに顔を紅潮させている。";
                    break;

                case PersonalityType.Apathetic: // 5. 無感動
                    Breasts = 0.5;
                    Armpits = 0.5;
                    Vagina = 1.5;
                    Womb = 2.0;
                    BaseExcitement = 0.5;// 鉄の自制（感情凍結）
                    SpecialTrait = ExcitementSpecialTrait.None;
                    personalityText = "まるで感情が抜け落ちたような瞳だ…";
                    break;

                case PersonalityType.Arrogant: // 6. 傲慢
                    Breasts = 0.6;
                    Clitoris = 0.6;
                    Mouth = 1.5;
                    Anus = 2.0;
                    BaseExcitement = 0.8;// 自己抑制
                    SpecialTrait = ExcitementSpecialTrait.ShameLover;// プライドをへし折られる屈辱に弱い
                    personalityText = "こちらを完全に見下している…";
                    break;

                case PersonalityType.Fastidious: // 7. 潔癖
                    Breasts = 0.8;
                    Armpits = 0.8;
                    Mouth = 1.8;
                    Anus = 2.0;
                    BaseExcitement = 0.5;// 鉄の自制
                    SpecialTrait = ExcitementSpecialTrait.ImmoralPleasure;
                    personalityText = "不潔なものを極端に嫌悪している…";
                    break;

                case PersonalityType.Bluffing: // 8. 虚勢 (ツンデレ/むっつり)
                    Mouth = 0.8;
                    Anus = 0.8;
                    Breasts = 1.8;
                    Clitoris = 2.0;
                    BaseExcitement = 1.2;// 隠れ欲情（心は拒んでも体は熱を持つ）
                    SpecialTrait = ExcitementSpecialTrait.ShameLover;
                    personalityText = "強がっているが、時折声が震えている…";
                    break;

                case PersonalityType.Devoted: // 9. 献身
                    Armpits = 0.8;
                    Clitoris = 0.8;
                    Breasts = 1.5;
                    Womb = 1.8;
                    SpecialTrait = ExcitementSpecialTrait.SubmissionDesire;
                    personalityText = "両手を組み祈りを捧げている。";
                    break;

                case PersonalityType.Stubborn: // 10. 頑固
                                               // すべての初見刺激に強い
                    Breasts = 0.8;
                    Clitoris = 0.8;
                    Vagina = 0.8;
                    BaseExcitement = 0.8;// 自己抑制
                    SpecialTrait = ExcitementSpecialTrait.None;
                    personalityText = "固く唇を噛み締め強固な意志の盾を構えている。";
                    break;

                case PersonalityType.Hedonistic: // 11. 享楽的
                    Breasts = 1.5;
                    Clitoris = 1.5;
                    Vagina = 1.5;
                    BaseExcitement = 1.5;// 生来の好色
                    SpecialTrait = ExcitementSpecialTrait.ImmoralPleasure;
                    personalityText = "未知の刺激への期待に頬を火照らせている。";
                    break;

                case PersonalityType.Cowardly: // 12. 臆病
                    Vagina = 0.8;
                    Womb = 0.8;
                    Armpits = 1.5;
                    Anus = 1.8;
                    BaseExcitement = 1.2;// 隠れ欲情（恐怖が熱に変換されやすい）
                    SpecialTrait = ExcitementSpecialTrait.SubmissionDesire;
                    personalityText = "すでに心が壊れんばかりに怯えきっている。";
                    break;
            }
        }
    }

}