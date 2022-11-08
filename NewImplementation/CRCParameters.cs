namespace _5Sem_Seti_Lab3.NewImplementation
{
    public class CRCParameters
    {
        public uint Polynom { get; set; }
        public uint RegisterInitial { get; set; } = 0;
        public bool IsInputReflection { get; set; } = false;
        public bool IsOutputReflection { get; set; } = false;
        public uint OutputSum { get; set; } = 0;
    }
}
