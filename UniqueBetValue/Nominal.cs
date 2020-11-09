namespace UniqueBetValue
{
    public class Nominal
    {
        //{ "recno" : "1", "extra_info":
        //{"nominalParams": {"betValue": 0.2, "lineCount": "8", "difficulty": "low"}}
        //,"nominal_label":"8_low_0.2","bet_value":"0.20"}

        public int Recno { get; set; }
        public Extra_info Extra_Info { get; set; }

        public string Nominal_label { get; set; }
    }
}
