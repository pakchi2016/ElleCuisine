using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace MaidensRecipe.Models
{
    public class BodyPart : INotifyPropertyChanged
    {
        // 部位の名前（例: 口、胸、尻穴など）
        public string Name { get; set; } = string.Empty;
        public string PartId { get; set; } = string.Empty;
        public double staticLocalPleasure { get; set; } = 1.0; // 性格による部位ごとの静的倍率

        public Func<bool> CanTarget { get; set; }

        private bool _isTargeted;
        public bool IsTargeted
        {
            get => _isTargeted;
            set
            {
                if (_isTargeted == value) return;

                // 新たに選択（True）されようとした時、上限チェック関数を実行
                if (value == true && CanTarget != null)
                {
                    // 上限オーバーなら
                    if (CanTarget() == false) { OnPropertyChanged(); return; }
                }

                _isTargeted = value;
                OnPropertyChanged();
            }
        }

        // ◆ 快楽値
        public int MaxPleasure { get; set; } = 100; // 快楽値の絶頂閾値（例: 100で絶頂）
        private int _currentPleasure = 0;
        public int CurrentPleasure
        {
            get => _currentPleasure;
            set { _currentPleasure = value; OnPropertyChanged(); }
        }

        // この搾取フェーズ中だけの絶頂回数（終わったら0）
        private int _sessionOrgasmCount;
        public int SessionOrgasmCount
        {
            get => _sessionOrgasmCount;
            set { _sessionOrgasmCount = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Sensitivity));
                OnPropertyChanged(nameof(SensitivityBuff));
            }
        }

        // 生涯の累計絶頂回数（セーブデータに残る永続データ）
        public int TotalOrgasmCount { get; set; }

        // ◆ 感度 (enum)
        private string _sensitivity = "";
        public string Sensitivity
        {
            get => _sensitivity;
            set {
                _sensitivity = SessionOrgasmCount switch
                {
                    < 10 => "",
                    < 20 => "[敏感]",
                    < 30 => "[過敏]",
                    _ => "[依存]"
                };
                OnPropertyChanged(); 
            }
        }

        // ◆ 感度バフ (double)
        private double _sensitivityBuff = 1.0;
        public double SensitivityBuff
        {
            get => _sensitivityBuff;
            set {
                _sensitivityBuff = SessionOrgasmCount switch
                {
                    < 10 => 1,
                    < 20 => 1.5,
                    < 30 => 2.0,
                    _ => 3.0
                };
                OnPropertyChanged();
            }
        }

        // この部位が現在ターゲット可能かどうか（UIのチェックボックスの有効/無効にバインドするためのプロパティ）
        public bool IsUnlocked { get; set; } = true;

        // 変化のあったプロパティをUIに通知するためのメソッド
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class BodyPartsData
    {
        // ◆ 各部位の初期化
        public BodyPart Mouth { get; set; } = new BodyPart { Name = "口", PartId = "Mouth"};
        public BodyPart Urethra { get; set; } = new BodyPart { Name = "尿道", PartId = "Urethra"};
        public BodyPart Breasts { get; set; } = new BodyPart { Name = "胸", PartId = "Breasts" };
        public BodyPart Anus { get; set; } = new BodyPart { Name = "尻穴", PartId = "Anus" };
        public BodyPart Clitoris { get; set; } = new BodyPart { Name = "クリトリス", PartId = "Clitoris" };
        public BodyPart Armpits { get; set; } = new BodyPart { Name = "脇", PartId = "Armpits" };
        public BodyPart Vagina { get; set; } = new BodyPart { Name = "膣", PartId = "Vagina" };
        public BodyPart Navel { get; set; } = new BodyPart { Name = "へそ", PartId = "Navel" };
        public BodyPart Womb { get; set; } = new BodyPart { Name = "子宮", PartId = "Womb" };
        public BodyPart Feet { get; set; } = new BodyPart { Name = "足", PartId = "Feet" };

        public int MaxTargetCount { get; set; } = 1;
        public BodyPartsData(PersonalityData personality)
        {
            Mouth.staticLocalPleasure = personality.Mouth;
            Urethra.staticLocalPleasure = personality.Urethra;
            Breasts.staticLocalPleasure = personality.Breasts;
            Anus.staticLocalPleasure = personality.Anus;
            Clitoris.staticLocalPleasure = personality.Clitoris;
            Armpits.staticLocalPleasure = personality.Armpits;
            Vagina.staticLocalPleasure = personality.Vagina;
            Navel.staticLocalPleasure = personality.Navel;
            Womb.staticLocalPleasure = personality.Womb;
            Feet.staticLocalPleasure = personality.Feet;

            foreach (var part in GetAllParts())
            {
                part.CanTarget = () =>
                {
                    // 現在ターゲットされている部位の数を数え、上限未満ならTrue（許可）を返す
                    int currentCount = GetAllParts().Count(p => p.IsTargeted);
                    return currentCount < MaxTargetCount;
                };
            }
        }

        // ◆ 卿の「連鎖絶頂」システムのための強力なヘルパーメソッド
        // 全10部位をリストとして取得し、C#側で一括計算（ループ処理）しやすくします
        public IEnumerable<BodyPart> GetAllParts()
        {
            yield return Mouth;
            yield return Urethra;
            yield return Breasts;
            yield return Anus;
            yield return Clitoris;
            yield return Armpits;
            yield return Vagina;
            yield return Navel;
            yield return Womb;
            yield return Feet;
        }
    }

}
