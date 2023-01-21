using FluentScheduler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

JobManager.Initialize();

JobManager.AddJob(
    () =>
    {
        var filePath = @"C:\temp\contoh.txt";
        var textToWrite = @$"Hai, saya ulong :D, sekarang jam {DateTime.Now.ToLongTimeString()}";

        File.WriteAllText(filePath, textToWrite); 
    },
    s => s.ToRunEvery(10).Seconds()
);

app.Run();
