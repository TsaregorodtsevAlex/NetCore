namespace NetCoreDataAccessTests.Specifications
{
    public class SpectificationTestEntity
    {
        public static SpectificationTestEntity Create(int id, string code)
        {
            return new SpectificationTestEntity
            {
                Code = code,
                Id = id
            };
        }

        public int Id { get; set; }
        public string Code { get; set; }
    }
}
