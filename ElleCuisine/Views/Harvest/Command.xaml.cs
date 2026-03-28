using MaidensRecipe.Models; // Maidenクラスの参照
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MaidensRecipe.Views.Harvest
{
    public partial class Command : UserControl
    {
        private Random _rnd = new Random();
        public Command() => InitializeComponent();

        // 1. 物理テスト: スタミナを削り、胸とクリトリスの数値を上げる
        private void BtnTestPhysical_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is Maiden target)
            {
                // ① 現在 IsTargeted が True になっている部位を「1つだけ」探し出す（複数選択不可の制限）
                var targetedPart = target.parts.GetAllParts().FirstOrDefault(p => p.IsTargeted);

                if (targetedPart != null)
                {
                    // ==================================================
                    // 1. 基礎値の決定 (触手の威力 + 乱数の揺らぎ)
                    // ==================================================
                    int basePleasure = 12 + _rnd.Next(-3, 4); // 9 〜 15

                    // ==================================================
                    // 2. 静的倍率 (先天的な理性・性格)
                    // ==================================================
                    double staticGlobal = target.maidenBase.staticGlobalPleasure; // 禁欲度による全体倍率
                    double staticLocal = targetedPart.staticLocalPleasure; // 性格による部位弱点

                    // ==================================================
                    // 3. 動的倍率 (後天的な本能・開発度)
                    // ==================================================
                    double dynamicGlobal = target.maidenBase.DynamicGlobalPleasure; // 興奮度に応じた全体倍率
                    double dynamicLocal = targetedPart.SensitivityBuff; // 感度による倍率

                    // ==================================================
                    // 4. 最終計算と反映
                    // ==================================================
                    double finalMultiplier = staticGlobal * staticLocal * dynamicGlobal * dynamicLocal;
                    int finalPleasure = (int)(basePleasure * finalMultiplier);

                    targetedPart.CurrentPleasure += finalPleasure;

                    // ==================================================
                    // 5. 絶頂(Orgasm) 判定
                    // ==================================================
                    if (targetedPart.CurrentPleasure >= targetedPart.MaxPleasure)
                    {
                        // 絶頂回数のインクリメント
                        targetedPart.SessionOrgasmCount += 1; // 今セッションでの絶頂回数
                        targetedPart.TotalOrgasmCount += 1;  // 累計絶頂回数

                        // 絶頂によるスタミナ（防壁）ダメージ計算
                        int baseOrgasmDamage = 30; // 絶頂の基本ダメージ
                        int actualStaDamage = (int)(baseOrgasmDamage * target.maidenBase.AsceticStaminaDamage);
                        target.maidenBase.CurrentStamina = Math.Max(0, target.maidenBase.CurrentStamina - actualStaDamage);

                        target.maidenBase.Excitement = Math.Max(0, target.maidenBase.Excitement - 30); // 絶頂による興奮の減少
                        targetedPart.CurrentPleasure = target.maidenBase.Excitement / 2; // 絶頂後は快楽が興奮の半分まで減少

                        // TODO: ここに「中央ペイン（ログ）へ絶頂テキストを出力する」処理を後ほど追加します
                    }
                    else
                    {
                        // ==========================================
                        // 寸止め（Edging）による興奮上昇の処理
                        // ==========================================
                        int baseExcitementUp = 10; // 基本の興奮値上昇量

                        // 寸止め状態の部位があるかどうかをチェック
                        foreach (var part in target.parts.GetAllParts())
                        {
                            // 快楽値が最大の80%以上の部位がある場合、寸止め状態と判定
                            double partPleasureRatio = (double)part.CurrentPleasure / part.MaxPleasure;
                            if (partPleasureRatio >= 0.8)
                            {
                                // 性格データに応じた寸止め部位の追加興奮値を取得
                                double edgingExcitementUp = baseExcitementUp * target.maidenBase.excitementBuff;
                                // 興奮値を追加
                                target.maidenBase.Excitement += (int)edgingExcitementUp;

                                // ★ 性癖（特殊バフ）の判定例
                                // ※ここでは例として、選んだコマンドが「屈辱的」で、乙女が「羞恥嗜好」なら追加ボーナス
                                // if (currentCommandType == CommandType.Humiliation && pData.SpecialTrait == ExcitementSpecialTrait.ShameLover)
                                // {
                                //     finalExcitementUp += 15; // 性癖にブチ刺さったので爆増！
                                // }


                            }
                        }
                    }

                }
            }
        }

        private void BtnTestState_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}