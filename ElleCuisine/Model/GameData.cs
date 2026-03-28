using MaidensRecipe.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MaidensRecipe.Models
{
    // ゲームの全データを保持し、UIへ変更を通知する総本山
    public class GameData : INotifyPropertyChanged
    {
        public GameData()
        {
            // 触手の全5段階をリストに登録。初期状態では第1段階のみ IsUnlocked = true に設定します。
            TentacleTechTree.Add(new TentacleData { StageLevel = 1, Name = "[第1段階] 繊毛型触手", IsUnlocked = true, UnlockCondition = "初期装備" });
            TentacleTechTree.Add(new TentacleData { StageLevel = 2, Name = "[第2段階] 吸盤・振動型", IsUnlocked = false, UnlockCondition = "解放条件: コロニー設備『吸着器官』の研究" });
            TentacleTechTree.Add(new TentacleData { StageLevel = 3, Name = "[第3段階] 張形・肉壁型", IsUnlocked = false, UnlockCondition = "解放条件: コロニー設備『強靭な筋繊維』の研究" });
            TentacleTechTree.Add(new TentacleData { StageLevel = 4, Name = "[第4段階] 陰茎・白濁液型", IsUnlocked = false, UnlockCondition = "解放条件: コロニー設備『擬似生殖機能』の研究" });
            TentacleTechTree.Add(new TentacleData { StageLevel = 5, Name = "[第5段階] 極細管・侵襲型", IsUnlocked = false, UnlockCondition = "解放条件: コロニー設備『極細神経接続』の研究" });
        }

        // ① 待機フェーズ用：収容所の乙女リスト（初期状態は空っぽ）
        public ObservableCollection<Maiden> PrisonRoster { get; set; } = new ObservableCollection<Maiden>();

        // ② 捕獲フェーズ用：侵略中の敵パーティリスト（初期状態は空っぽ）
        public ObservableCollection<Maiden> InvaderParty { get; set; } = new ObservableCollection<Maiden>();

        // ③ 搾取フェーズ用：現在の獲物（単一の枠。初期状態は null）
        private Maiden _harvestTarget;
        public Maiden HarvestTarget
        {
            get => _harvestTarget;
            set { _harvestTarget = value; OnPropertyChanged(); }
        }

        // UIへ「値が変わった」と知らせるためのWPF必須イベント
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ObservableCollection<TentacleData> TentacleTechTree { get; set; } = new ObservableCollection<TentacleData>();

    }


    }