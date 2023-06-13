using System.ComponentModel.DataAnnotations;

namespace CodebridgeTest.Domain.Options;

public class ServiceInfo
{
    [Required] 
    public string Version { get; set; } = string.Empty;
}