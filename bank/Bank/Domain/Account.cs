using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Bank.Domain;

[Index(nameof(Number), IsUnique = true)]
public class Account
{
    [Key]
    public long Id { get; set; }

    [Range(10000, 99999)]
    public int Number { get; set; }

    [Range(0, long.MaxValue)]
    public long Balance { get; set; }
}