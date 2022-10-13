using PostTask.RestService.Domain;

namespace PostTask.RestService.Tests.DomainTests;
public class TaskTests
{
    private readonly Domain.Task _task = new Domain.Task();
    [Fact]
    public void ModifyTaskItem_ModifyNotContainedItem_ShouldReturnNull()
    {
        //arrange
        var taskItem = new TaskItem();
        //act
        var modifiedTask = _task.ModifyTaskItem(taskItem.Id, updateState: item =>
        {
            item.From = DateTime.MinValue;
            item.To = DateTime.MaxValue;
            item.IsDone = true;
        });
        //assert
        Assert.DoesNotContain(
            expected: taskItem,
            collection: _task);
        Assert.Null(modifiedTask);
    }
    [Fact]
    public void ModifyTaskItem_ModifyContainedItem_ShouldReturnTaskItem()
    {
        //arrange
        var taskItem = new TaskItem();
        _task.Add(taskItem);
        //act
        var modifiedTask = _task.ModifyTaskItem(taskItem.Id, updateState: item =>
        {
            item.From = DateTime.MinValue;
            item.To = DateTime.MaxValue;
            item.IsDone = true;
        });
        //assert
        Assert.True(
            ReferenceEquals(taskItem, modifiedTask));
        Assert.Contains(
            expected: taskItem,
            collection: _task);
    }
}