using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Common;

public class DateOnlySerializer : StructSerializerBase<DateOnly>
{
    public string Format { get; } = "yyyy-MM-dd";
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateOnly value) => context.Writer.WriteString(value.ToString(Format));
    public override DateOnly Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args) => DateOnly.ParseExact(context.Reader.ReadString(), Format);
}
