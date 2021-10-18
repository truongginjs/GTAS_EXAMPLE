namespace BaseService.Models
{
    public class AQLLibrary : BaseModel
    {
        public string Category { get; set; }
        public string SeqNo { get; set; }
        public string LotSize { get; set; }
        public string SampleSize { get; set; }
        public string Accept { get; set; }
        public string Reject { get; set; }
    }
}

/*
id	category	seq_no	lot_size	sample_size	accept	reject
10	AQL 2.5	2	51-90	13	1	2
11	AQL 2.5	3	91-150	20	1	2
12	AQL 2.5	4	151-280	32	2	3
13	AQL 2.5	5	281-500	50	3	4
14	AQL 2.5	6	501-1.200	80	5	6
15	AQL 2.5	7	1.201-3.200	125	7	8
16	AQL 2.5	8	3.201-10.000	200	10	11
17	AQL 2.5	9	15.001-500.000	315	14	15
18	AQL 2.5	10	500,000 over	500	21	22
19	Sizeset-Pilot	1	10	10	1	2
20	Sizeset-Pilot	2	20	20	1	2
21	Sizesfit-Pilot	3	30	30	2	3
*/