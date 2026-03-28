using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MaidensRecipe.Models
{
    // 乙女の基本情報を定義するクラス
    public class MaidenBase : INotifyPropertyChanged
    {
        // ◆ 乙女の基本情報
        public string Name { get; set; } = "";        // 名前（ジョーカー化で付与される）
        public string JobClass { get; set; } = "";    // 職業（姫騎士、僧侶など）
        public double Asceticism { get; set; } = 50;     // 禁欲度（0〜100）
        public double staticGlobalPleasure { get; set; } = 1.0; // 性格による全体の快楽蓄積倍率
        public double AsceticStaminaDamage { get; set; } = 1.0; // 禁欲度によるスタミナダメージ倍率
        public double excitementBuff { get; set; } = 1.0; // 興奮度の蓄積しやすさ
        public string personalityText { get; set; } = "初見";　

        // ◆ ステータス（最大値）
        public double MaxHP { get; set; } // HP（体力）
        public double MaxMP { get; set; } // MP（精神力）
        public double MaxStamina { get; set; }     // STA（物理防壁）
        public double MaxMental { get; set; }      // MEN（精神防壁）
        public double MaxDignity { get; set; }     // DIG（尊厳防壁）

        public MaidenBase(MaidenJob job, PersonalityData personality)
        {
            Name = job.jobData.Name;
            JobClass = job.jobData.JobName;
            Asceticism = job.jobData.Asceticism;
            personalityText = personality.personalityText;
            staticGlobalPleasure = 1.5  - ( Asceticism / 100 );
            AsceticStaminaDamage = 0.5 + ( Asceticism / 100 );
            excitementBuff = personality.BaseExcitement;
            MaxHP = job.jobData.BaseHP;
            MaxMP = job.jobData.BaseMP;
            MaxStamina = job.jobData.BaseStamina;
            MaxMental = job.jobData.BaseMental;
            MaxDignity = job.jobData.BaseDignity;
            _currentHP = MaxHP;
            _currentHP = MaxHP;
            _currentStamina = MaxStamina;
            _currentMental = MaxMental;
            _currentDignity = MaxDignity;
        }

        // ◆ ステータス（現在値）
        private double _currentHP { get; set; }
        public double CurrentHP
        {
            get => _currentHP;
            set { _currentHP = value; OnPropertyChanged(); }
        }

        private double _currentMP { get; set; }
        public double CurrentMP
        {     get => _currentMP;
            set { _currentMP = value; OnPropertyChanged(); }
        }

        private double _currentStamina;
        public double CurrentStamina
        {
            get => _currentStamina;
            set { _currentStamina = value; OnPropertyChanged(); }
        }
        private double _currentMental { get; set; }
        public double CurrentMental
        {     get => _currentMental;
            set { _currentMental = value; OnPropertyChanged(); }
        }

        private double _currentDignity;
        public double CurrentDignity
        {
            get => _currentDignity;
            set { _currentDignity = value; OnPropertyChanged(); }
        }

        // ★ プレイヤー向け表示：禁欲度を「抵抗感」として匂わせるテキスト
        public string AsceticismHintText
        {
            get
            {
                if (Asceticism >= 90) return "【極めて強固な抵抗】";
                if (Asceticism >= 70) return "【強い警戒心】";
                if (Asceticism >= 40) return "【標準的な羞恥心】";
                return "【どこか危うい隙】"; // 禁欲度が低い場合
            }
        }

        // 興奮度（0〜100）高いと快楽の蓄積にボーナスがかかります。
        private int _excitement;
        public int Excitement
        {
            get => _excitement;
            set
            {
                _excitement = Math.Max(0, Math.Min(100, value));
                OnPropertyChanged();
            }
        }

        public double dynamicGlobalPleasure => 1.0 + (_excitement / 100.0);

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}