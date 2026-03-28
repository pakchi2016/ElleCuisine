using MaidensRecipe.Models;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MaidensRecipe.Views
{
    public partial class MainWindow : Window
    {
        private GameData _gameData = new GameData();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = _gameData;
        }

        // 1. 待機フェーズへ切り替え
        private void BtnPhaseStandby_Click(object sender, RoutedEventArgs e)
        {
            // Tactical (待機) を表示
            TacCentral.Visibility = Visibility.Visible;
            TacSide.Visibility = Visibility.Visible;
            TacCmd.Visibility = Visibility.Visible;

            // 他を非表示
            BatCentral.Visibility = Visibility.Collapsed;
            BatSide.Visibility = Visibility.Collapsed;
            BatCmd.Visibility = Visibility.Collapsed;
            HarCentral.Visibility = Visibility.Collapsed;
            HarSide.Visibility = Visibility.Collapsed;
            HarCmd.Visibility = Visibility.Collapsed;
        }

        // 2. 捕獲フェーズへ切り替え
        private void BtnPhaseCapture_Click(object sender, RoutedEventArgs e)
        {
            BatCentral.Visibility = Visibility.Visible;
            BatSide.Visibility = Visibility.Visible;
            BatCmd.Visibility = Visibility.Visible;

            TacCentral.Visibility = Visibility.Collapsed;
            TacSide.Visibility = Visibility.Collapsed;
            TacCmd.Visibility = Visibility.Collapsed;
            HarCentral.Visibility = Visibility.Collapsed;
            HarSide.Visibility = Visibility.Collapsed;
            HarCmd.Visibility = Visibility.Collapsed;
        }

        // 3. 搾取フェーズへ切り替え
        private void BtnPhaseExploit_Click(object sender, RoutedEventArgs e)
        {
            HarCentral.Visibility = Visibility.Visible;
            HarSide.Visibility = Visibility.Visible;
            HarCmd.Visibility = Visibility.Visible;

            TacCentral.Visibility = Visibility.Collapsed;
            TacSide.Visibility = Visibility.Collapsed;
            TacCmd.Visibility = Visibility.Collapsed;
            BatCentral.Visibility = Visibility.Collapsed;
            BatSide.Visibility = Visibility.Collapsed;
            BatCmd.Visibility = Visibility.Collapsed;
        }

        // 1. 捕獲フェーズのリストに敵を生成する（エンカウント）
        private void BtnGenerateEnemy_Click(object sender, RoutedEventArgs e)
        {
            // ★ 工場（MaidenFactory）に名前とジョブを伝えて、完成品を受け取るだけ！
            var newEnemy = MaidenFactory.Generate();

            // 敵パーティリストへ追加
            _gameData.InvaderParty.Add(newEnemy);
        }

        // 2. 敵リストから1名を抜き取り、搾取フェーズのコックピットへ縛り付ける
        private void BtnCapture_Click(object sender, RoutedEventArgs e)
        {
            if (_gameData.InvaderParty.Count > 0)
            {
                // リストの先頭にいる乙女のインスタンスを確保
                var targetMaiden = _gameData.InvaderParty[0];

                // 捕獲フェーズの画面（リスト）からは消去
                _gameData.InvaderParty.RemoveAt(0);

                // 状態を書き換え、搾取フェーズ用の単一枠へ「移動」させる
                targetMaiden.CurrentState = MaidenState.Harvesting;
                _gameData.HarvestTarget = targetMaiden;

                // ※これにより、搾取フェーズ画面の中央にアンナの情報がバインドされます
            }
        }

        // 3. 搾取を終え、コックピットから外して収容所へ放り込む
        private void BtnImprison_Click(object sender, RoutedEventArgs e)
        {
            if (_gameData.HarvestTarget != null)
            {
                // コックピットにいる乙女のインスタンスを確保
                var capturedMaiden = _gameData.HarvestTarget;

                // コックピットの枠を空（null）にする
                _gameData.HarvestTarget = null;

                // 状態を書き換え、待機フェーズの収容所リストへ「移動」させる
                capturedMaiden.CurrentState = MaidenState.Imprisoned;
                _gameData.PrisonRoster.Add(capturedMaiden);

                // ※これにより、待機フェーズのリスト画面にアンナが追加されます
            }
        }
    }
}