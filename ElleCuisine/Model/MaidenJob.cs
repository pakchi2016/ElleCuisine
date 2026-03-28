using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace MaidensRecipe.Models
{
    public class JobData
    {
        public int JobID { get; set; } = 0; // ジョブID
        public string JobName { get; set; } = string.Empty; // ジョブ名
        public string Name { get; set; } = string.Empty; // 名前（ジョブに応じてランダム付与）
        public double Asceticism { get; set; } = 50; // 禁欲度（0〜100）
        public double BaseHP { get; set; } = 100; // 基本HP
        public double BaseMP { get; set; } = 50; // 基本MP
        public double BaseStamina { get; set; } = 100; // 基本スタミナ
        public double BaseMental { get; set; } = 100; // 基本精神力
        public double BaseDignity { get; set; } = 100; // 基本尊厳
        public PersonalityType Personality { get; set; } = PersonalityType.Pure;
    }

    public class MaidenJob
    {
        // 乱数生成器
        private static Random _rnd = new Random();

        public JobData jobData = new JobData();
        public MaidenJob(int id)
        {
            var saintNames = new List<string> { "セシリア", "マリア", "エリザベス", "アナスタシア", "カタリナ", "フランチェスカ", "セレスティア", "ソフィア", "テレサ", "クラリス", "ユスティーナ", "セシリア", "マリアンヌ", "ルーチェ", "エレナ", "プリシラ" };
            var knightNames = new List<string> { "アリス", "イザベラ", "エレナ", "オリヴィア", "ビアンカ", "シルヴィア",  "ヴァネッサ", "ヒルダ", "ブリジット", "マティルダ", "フレイヤ",  "ジャンヌ" };
            var kunoichiNames = new List<string> { "ユキ", "サクラ", "ミドリ", "アヤカ", "レイナ", "ナツキ" };
            var warriorNames = new List<string> { "サラ", "レオナ", "アデライド", "イグレーヌ", "ロザリア", "カトレア", "シャーロット", "オリヴィア", "イザベラ", "リーゼロッテ", "ファルネーゼ", "ルクレーツィア", "カミーラ", "ベアトリス" };
            var mageNames = new List<string> { "シルヴィア", "エミリア", "フィオナ", "ルナ", "セレナ", "ノエル", "リリアナ", "ルシエラ", "クロエ", "ディアナ", "ミレイユ", "オクタヴィア", "ダフネ", "グレイス", "ユリア", "エルザ" };
            var villagerNames = new List<string> { "アメリア", "フィオナ", "ノエル", "セリア", "ラウラ", "リノア", "エヴリン", "ジル", "ニーナ", "リタ" };

            jobData.JobID = id;
            switch (id)
            {
                case 0: // 聖女
                    jobData.JobName = "聖女";
                    jobData.Name = saintNames[_rnd.Next(saintNames.Count)];
                    jobData.Asceticism = _rnd.Next(85, 101);
                    jobData.BaseHP = 120;
                    jobData.BaseMP = 80;
                    jobData.BaseStamina = 80;
                    jobData.BaseMental = 120;
                    jobData.BaseDignity = 150;
                    jobData.Personality = ChooseRandomPersonality( PersonalityType.Pure, PersonalityType.Devoted, PersonalityType.Fastidious);
                    break;
                case 1: // 姫騎士
                    jobData.JobName = "姫騎士";
                    jobData.Name = knightNames[_rnd.Next(knightNames.Count)];
                    jobData.Asceticism = _rnd.Next(70, 91);
                    jobData.BaseHP = 150;
                    jobData.BaseMP = 40;
                    jobData.BaseStamina = 120;
                    jobData.BaseMental = 100;
                    jobData.BaseDignity = 120;
                    jobData.Personality = ChooseRandomPersonality( PersonalityType.Defiant, PersonalityType.Arrogant, PersonalityType.Stubborn, PersonalityType.Bluffing);
                    break;
                case 2: // くノ一
                    jobData.JobName = "くノ一";
                    jobData.Name = kunoichiNames[_rnd.Next(kunoichiNames.Count)];
                    jobData.Asceticism = _rnd.Next(60, 86);
                    jobData.BaseHP = 100;
                    jobData.BaseMP = 60;
                    jobData.BaseStamina = 100;
                    jobData.BaseMental = 80;
                    jobData.BaseDignity = 80;
                    jobData.Personality = ChooseRandomPersonality( PersonalityType.Apathetic, PersonalityType.Timid, PersonalityType.Cowardly);
                    break;
                case 3: // 戦士
                    jobData.JobName = "戦士";
                    jobData.Name = warriorNames[_rnd.Next(warriorNames.Count)];
                    jobData.Asceticism = _rnd.Next(50, 81);
                    jobData.BaseHP = 150;
                    jobData.BaseMP = 30;
                    jobData.BaseStamina = 150;
                    jobData.BaseMental = 70;
                    jobData.BaseDignity = 100;
                    jobData.Personality = ChooseRandomPersonality( PersonalityType.Defiant, PersonalityType.Arrogant, PersonalityType.Stubborn, PersonalityType.Bluffing);
                    break;
                case 4: // 魔法使い
                    jobData.JobName = "魔法使い";
                    jobData.Name = mageNames[_rnd.Next(mageNames.Count)];
                    jobData.Asceticism = _rnd.Next(40, 71);
                    jobData.BaseHP = 80;
                    jobData.BaseMP = 150;
                    jobData.BaseStamina = 60;
                    jobData.BaseMental = 120;
                    jobData.BaseDignity = 90;
                    jobData.Personality = ChooseRandomPersonality( PersonalityType.Pure, PersonalityType.Devoted, PersonalityType.Fastidious);
                    break;
                default: // 村娘
                    jobData.JobName = "村娘";
                    jobData.Name = villagerNames[_rnd.Next(villagerNames.Count)];
                    jobData.Asceticism = _rnd.Next(30, 71);
                    jobData.BaseHP = 100;
                    jobData.BaseMP = 50;
                    jobData.BaseStamina = 80;
                    jobData.BaseMental = 80;
                    jobData.BaseDignity = 70;
                    jobData.Personality = ChooseRandomPersonality( PersonalityType.Emotional, PersonalityType.Hedonistic, PersonalityType.Timid);
                    break;
            }
        }
        private static PersonalityType ChooseRandomPersonality(params PersonalityType[] candidates)
        {
            int index = _rnd.Next(candidates.Length);
            return candidates[index];
        }
    }
}
