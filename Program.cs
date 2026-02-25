using Byte_Backend.Dados;
using Byte_Backend.Interfaces;
using Byte_Backend.Repositories;
using Byte_Backend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ByteDbContext>(options =>
    options.UseNpgsql(connectionString));

// CORS Config
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Injeção de Dependência - Repositories
builder.Services.AddScoped<ICargoRepository, CargoRepository>();
builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
builder.Services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IOpcaoProdutoRepository, OpcaoProdutoRepository>();
builder.Services.AddScoped<ICartaoComandaRepository, CartaoComandaRepository>();
builder.Services.AddScoped<IComandaRepository, ComandaRepository>();
builder.Services.AddScoped<IItemComandaRepository, ItemComandaRepository>();
builder.Services.AddScoped<IFilaCozinhaRepository, FilaCozinhaRepository>();
builder.Services.AddScoped<IProdutoImagemRepository, ProdutoImagemRepository>();
builder.Services.AddScoped<ISolicitacaoAtendimentoRepository, SolicitacaoAtendimentoRepository>();
builder.Services.AddScoped<IUsuarioPermissaoRepository, UsuarioPermissaoRepository>();

// Injeção de Dependência - Services
builder.Services.AddScoped<CargoService>();
builder.Services.AddScoped<PessoaService>();
builder.Services.AddScoped<FuncionarioService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<OpcaoProdutoService>();
builder.Services.AddScoped<CartaoComandaService>();
builder.Services.AddScoped<ComandaService>();
builder.Services.AddScoped<ItemComandaService>();
builder.Services.AddScoped<FilaCozinhaService>();
builder.Services.AddScoped<ProdutoImagemService>();
builder.Services.AddScoped<SolicitacaoAtendimentoService>();
builder.Services.AddScoped<UsuarioPermissaoService>();

// Configuração de Autenticação JWT
var jwtSecretKey = builder.Configuration["JwtSettings:SecretKey"] 
    ?? throw new InvalidOperationException("JWT SecretKey não configurada.");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Byte API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.OperationFilter<Byte_Backend.Configuration.AuthorizeCheckOperationFilter>();
    
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Byte API v1");
    c.RoutePrefix = "swagger";
});

app.UseCors("AllowSpecificOrigin");

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();