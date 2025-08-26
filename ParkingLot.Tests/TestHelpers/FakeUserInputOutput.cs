using ParkingLot.Common.Interfaces;

namespace ParkingLot.Tests.TestHelpers;

public class FakeUserInputOutput : IUserInputOutput
{
    private readonly Queue<string?> _inputs;

    private readonly List<string> _outputs = new();
    private readonly object _lock = new();

    public FakeUserInputOutput(IEnumerable<string?>? inputs = null)
    {
        _inputs = inputs is null ? new Queue<string?>() : new Queue<string?>(inputs);
    }

    public string? ReadLine()
    {
        lock (_lock)
        {
            if (_inputs.Count == 0) return null;
            return _inputs.Dequeue();
        }
    }

    public void WriteLine(string message)
    {
        lock (_lock)
        {
            _outputs.Add(message);
        }
    }

    public IReadOnlyList<string> Outputs
    {
        get { lock (_lock) { return _outputs.ToArray(); } }
    }

    public void AddInput(string? input)
    {
        lock (_lock) { _inputs.Enqueue(input); }
    }

    public void ClearOutputs()
    {
        lock (_lock) { _outputs.Clear(); }
    }

    public void Clear()
    {
        lock (_lock) { _outputs.Add("Console Cleared"); };
    }
}
