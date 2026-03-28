using System;
using System.Collections.Generic;

namespace MaidensRecipe.Models
{
    public static class MaidenFactory
    {

        // 乱数生成器（すべての生成で使い回します）
        private static Random _rnd = new Random();

        // 指定された名前とジョブから、パラメータが自動設定された乙女を錬成します
        public static Maiden Generate() => new Maiden(_rnd.Next(0, 7));

    }
}