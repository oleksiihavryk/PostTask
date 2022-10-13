using PostTask.RestService.Domain;
using PostTask.RestService.Domain.Exceptions;

namespace PostTask.RestService.Tests.DomainTests;
public class TaskItemTests
{
    private readonly TaskItem _taskItem = new TaskItem();
    private readonly DateTime _now = DateTime.Now;

    [Fact]
    public void ChangingTime_ToTimeSmallerThenFrom_ShouldReturnError()
    {
        //arrange
        var timeFrom = _now;
        var timeTo = _now - TimeSpan.FromDays(1);
        //act
        void Action()
        {
            _taskItem.ChangeTime(from: timeFrom, to: timeTo);
        }
        //assert
        Assert.Throws<IncorrectTimeInputException>(Action);
    }
    [Fact]
    public void ChangingTime_PassingCorrectTime_ShouldWorkCorrect()
    {
        //arrange
        var timeFrom = _now;
        DateTime? timeTo = null;
        //act
        _taskItem.ChangeTime(from: timeFrom, to: timeTo);
        //assert
        Assert.Equal(
            expected: timeTo,
            actual: _taskItem.To);
        Assert.Equal(
            expected: timeFrom,
            actual: _taskItem.From);
    }
}