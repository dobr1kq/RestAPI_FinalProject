using Domain.Readers;

namespace Test.Data;

public static class ReaderData
{
    public static Reader MainReader => Reader.New(
        ReaderId.New(),
        "Alice",
        "Johnson",
        "+38987654000"
    );
}