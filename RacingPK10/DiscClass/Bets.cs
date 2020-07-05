using System;
using System.Collections;
using System.Collections.Generic;

namespace RacingPK10.DiscClass
{
    public class Bets
    {
        public string url { get; set; }
        public BetsJson betsJson { get; set; }
    }
    public class BetsJson
    {
        public string lid { get; set; }
        public int isOddsAuto { get; set; }
        public string periods { get; set; }
        public object[][] orders { get; set; }
        public string Token { get; set; }
        public int tid { get; set; }
    }
    public class Order
    {
        public string Id { get; set; }
        public string Ranking { get; set; }
        public string Number { get; set; }
        public double Odds { get; set; }
        public int Money { get; set; }
    }
}