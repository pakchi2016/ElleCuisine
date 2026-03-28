using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MaidensRecipe.Models
{
    // 乙女が現在どの運命にあるかを示すラベル（同時には1つしかなれない）
    public enum MaidenState
    {
        Invading,       // 侵略中（捕獲フェーズの敵パーティにいる）
        Harvesting,     // 搾取中（搾取フェーズでコックピットに繋がれている）
        Imprisoned,     // 監禁中（待機フェーズ：未洗脳で抵抗中）
        Brainwashed,    // 洗脳済（待機フェーズ：眷属として労働可能）
        Joker,          // ジョーカー（待機フェーズ：ネームド昇格済）
        NemesisReserve, // ネメシス候補（逃亡または解放され、復讐の機を伺っている）
        Nemesis         // ネメシス化（強化され、再び罠へ侵攻してくる強敵）
    }

    // 乙女の基本情報を定義するクラス
    public class Maiden : INotifyPropertyChanged
    {
        // ◆ 乙女の運命状態
        public MaidenState CurrentState { get; set; } = MaidenState.Invading;

        // ◆ 乙女の職業クラス（姫騎士、僧侶など）をまとめたクラス
        public MaidenJob job { get; set; }
        // 乙女の性格タイプ（純粋、傲慢、潔癖など）
        public PersonalityData personality { get; set; } 

        // ◆ 乙女の基本情報（名前、職業など）をまとめたクラス
        public MaidenBase maidenBase { get; set; }

        // ◆ 乙女の部位ごとの快楽値や絶頂回数などをまとめたクラス
        public BodyPartsData parts { get; set; }

        public Maiden(int jobClass)
        {
            job = new MaidenJob(jobClass);
            personality = new PersonalityData(job);
            maidenBase = new MaidenBase(job, personality);
            parts = new BodyPartsData(personality);
        }

        // ◆ コロニー運営パラメーター
        public int MaintenanceCost { get; set; } // 消費生体エネルギー（EN/T）

        // ◆ メソッドの例：状態を書き換える
        public void ChangeState(MaidenState newState)
        {
            CurrentState = newState;
            // ※ここで、ジョーカー化したら名前をランダム付与するなどの処理を挟めますわ
        }

        // UIへ通知を送るためのWPF必須イベント
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
