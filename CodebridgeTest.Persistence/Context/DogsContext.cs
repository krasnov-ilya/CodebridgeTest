using CodebridgeTest.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodebridgeTest.Persistence.Context;

public class DogsContext : DbContext, IDogsContext

{
    
}