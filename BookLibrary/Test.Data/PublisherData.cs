using Domain.Publishers;

namespace Test.Data;

public static class PublisherData
{
    public static Publisher MainPublisher => Publisher.New(
        PublisherId.New(),
        "Test Publisher",
        "123 Publisher St."
    );
}