using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MaidensRecipe.Components
{
    public partial class StatusBar : UserControl
    {
        public StatusBar()
        {
            InitializeComponent();
        }

        // ① タイトル ("STA", "胸" など)
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(StatusBar), new PropertyMetadata(""));

        // ② サブタイトル ("(Lv.15) [敏感]" など。デフォルトは空)
        public string SubTitle
        {
            get { return (string)GetValue(SubTitleProperty); }
            set { SetValue(SubTitleProperty, value); }
        }
        public static readonly DependencyProperty SubTitleProperty =
            DependencyProperty.Register("SubTitle", typeof(string), typeof(StatusBar), new PropertyMetadata(""));

        // ③ 現在値と最大値
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(StatusBar), new PropertyMetadata(0));

        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(int), typeof(StatusBar), new PropertyMetadata(100));

        // ④ 色指定（文字色とゲージ色）
        public Brush TextColor
        {
            get { return (Brush)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }
        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register("TextColor", typeof(Brush), typeof(StatusBar), new PropertyMetadata(Brushes.White));

        public Brush BarColor
        {
            get { return (Brush)GetValue(BarColorProperty); }
            set { SetValue(BarColorProperty, value); }
        }
        public static readonly DependencyProperty BarColorProperty =
            DependencyProperty.Register("BarColor", typeof(Brush), typeof(StatusBar), new PropertyMetadata(Brushes.White));

        // ⑤ ゲージの太さ（防壁は6px、部位は8pxなど調整可能にする）
        public double BarHeight
        {
            get { return (double)GetValue(BarHeightProperty); }
            set { SetValue(BarHeightProperty, value); }
        }
        public static readonly DependencyProperty BarHeightProperty =
            DependencyProperty.Register("BarHeight", typeof(double), typeof(StatusBar), new PropertyMetadata(6.0));

        // ⑥ 数値(30/100など)を表示するかどうかのスイッチ（デフォルトは表示=true）
        public bool ShowValueText
        {
            get { return (bool)GetValue(ShowValueTextProperty); }
            set { SetValue(ShowValueTextProperty, value); }
        }
        public static readonly DependencyProperty ShowValueTextProperty =
            DependencyProperty.Register("ShowValueText", typeof(bool), typeof(StatusBar), new PropertyMetadata(true));
    }
}