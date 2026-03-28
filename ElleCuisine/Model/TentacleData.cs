using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MaidensRecipe.Models
{
    public class TentacleData : INotifyPropertyChanged
    {
        public string Name { get; set; }           // 触手の名前
        public string UnlockCondition { get; set; } // 未解放時のツールチップ説明
        public int StageLevel { get; set; }        // 第何段階か

        // ★ 研究開発によって切り替わる解放フラグ
        private bool _isUnlocked;
        public bool IsUnlocked
        {
            get => _isUnlocked;
            set { _isUnlocked = value; OnPropertyChanged(); }
        }

        // UIへ通知を送るための必須イベント
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}