using Auth.Domain.Users.Enums;
using ProtoBuf;
using ProtoBuf.Grpc;
using System.ServiceModel;

namespace Auth.Grpc.Contracts;

[ServiceContract]
public interface IPersonService
{
    [OperationContract]
    ValueTask<GetPersonResponse> GetPersonByUserIdAsync(GetPersonByUserIdRequest request, CallContext context = default);
}

[ProtoContract]
public class PersonInfo
{
    [ProtoMember(1)]
    public int Id { get; set; }
    [ProtoMember(2)]
    public string FirstName { get; set; }
    [ProtoMember(3)]
    public string LastName { get; set; }
    [ProtoMember(4)]
    public string Email { get; set; }
    [ProtoMember(5)]
    public Gender Gender { get; set; }
    [ProtoMember(6)]
    public Honorific? Honorific { get; set; }
    [ProtoMember(7)]
    public string? PictureUrl { get; set; }
    [ProtoMember(8)]
    public ProfessionalProfile? ProfessionalProfile { get; set; }
    [ProtoMember(9)]
    public int? UserId { get; set; }

}

[ProtoContract]
public class ProfessionalProfile
{
    [ProtoMember(1)]
    public string Position { get; set; } = default!;
    [ProtoMember(2)]
    public string CompanyName { get; set; } = default!;
    [ProtoMember(3)]
    public string Affiliation { get; set; } = default!;

}

[ProtoContract]
public class GetPersonByUserIdRequest
{
    [ProtoMember(1)]
    public int UserId { get; set; }
}

[ProtoContract]
public class GetPersonResponse
{
    [ProtoMember(1)]
    public PersonInfo PersonInfo { get; set; } = default!;
}

