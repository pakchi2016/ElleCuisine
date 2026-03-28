using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

namespace MaidensRecipe.Behaviors
{
    // プログレスバーの値を滑らかにアニメーションさせるための拡張機能
    public static class ProgressBarBehavior
    {
        // XAMLからバインディングするための「AnimatingValue」という新しいプロパティを定義
        public static readonly DependencyProperty AnimatingValueProperty =
            DependencyProperty.RegisterAttached(
                "AnimatingValue",
                typeof(double),
                typeof(ProgressBarBehavior),
                new PropertyMetadata(0.0, OnAnimatingValueChanged));

        public static double GetAnimatingValue(DependencyObject obj) => (double)obj.GetValue(AnimatingValueProperty);
        public static void SetAnimatingValue(DependencyObject obj, double value) => obj.SetValue(AnimatingValueProperty, value);

        // 値が変更された（例：1から11になった）瞬間に呼ばれる魔法の処理
        private static void OnAnimatingValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ProgressBar progressBar)
            {
                double targetValue = (double)e.NewValue;

                // アニメーションの作成（0.4秒かけて滑らかに変化させる）
                DoubleAnimation animation = new DoubleAnimation
                {
                    To = targetValue,
                    Duration = TimeSpan.FromSeconds(0.4),
                    // EasingFunctionを入れることで、最初は早く、終わり際で「ググッ…」と粘るような生々しい動きになります
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };

                // アニメーションを開始
                progressBar.BeginAnimation(RangeBase.ValueProperty, animation);
            }
        }
    }
}