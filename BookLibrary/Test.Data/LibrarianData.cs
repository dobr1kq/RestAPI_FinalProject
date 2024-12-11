using Domain.Librarians;

namespace Test.Data;

public static class LibrarianData
{
    public static Librarian MainLibrarian => Librarian.New(
        LibrarianId.New(),
        "Sarah",
        "Smith",
        "123-456-7890"
    );
}