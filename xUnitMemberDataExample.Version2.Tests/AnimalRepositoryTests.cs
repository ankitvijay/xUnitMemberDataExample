using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xunit;
using xUnitMemberDataExample.Version2.Tests;


public class AnimalRepositoryTests
{
    [Theory]
    [MemberData(nameof(GetFindAnimalData))]
    public void FindAnimalsReturnCorrectResult(AnimalRepositoryTestSource testData)
    {
        var repository = new AnimalRepository();

        // Act
        var result = repository.Find(testData.SearchCriteria);

        // Assert
        Assert.Equal(testData.ExpectedResult, result);
    }

    public static IEnumerable<object[]> GetFindAnimalData()
    {
        yield return WhenSearchTermDoesNotExist();
        yield return WhenIgnoreCaseIsSetToTrue();
        yield return WhenSearchTermIsPartialMatch();
    }

    private static object[] WhenSearchTermDoesNotExist()
    {
        return new object[]
        {
            new AnimalRepositoryTestSource(new SearchCriteria("FOX"), null)
        };
    }

    private static object[] WhenIgnoreCaseIsSetToTrue()
    {
        return new object[]
        {
            new AnimalRepositoryTestSource(new SearchCriteria("dog", true), "DOG")
        };
    }
    private static object[] WhenSearchTermIsPartialMatch()
    {
        return new object[]
        {
            new AnimalRepositoryTestSource(new SearchCriteria("IG", true), "TIGER")
        };
    }


    public class AnimalRepositoryTestSource : TestSource
    {
        public AnimalRepositoryTestSource(SearchCriteria searchCriteria, string expectedResult, [CallerMemberName]string testName = null)
            : base(testName)
        {
            SearchCriteria = searchCriteria;
            ExpectedResult = expectedResult;
        }

        public SearchCriteria SearchCriteria { get; }
        public string ExpectedResult { get; }
    }
}
