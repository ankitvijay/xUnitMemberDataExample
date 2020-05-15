using System.Collections.Generic;
using Xunit;


public class AnimalRepositoryTests
{
    [Theory]
    [MemberData(nameof(GetFindAnimalData))]
    public void FindReturnsCorrectResult(SearchCriteria searchCriteria, string expectedResult)
    {
        var repository = new AnimalRepository();

        // Act
        var result = repository.Find(searchCriteria);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    public static IEnumerable<object[]> GetFindAnimalData()
    {
        yield return new object[]
        {
                new SearchCriteria("FOX"),
                null
        };

        yield return new object[]
        {
                new SearchCriteria("dog", true),
                "DOG"
        };

        yield return new object[]
        {
                new SearchCriteria("IG"),
                "TIGER"
        };
    }
}
