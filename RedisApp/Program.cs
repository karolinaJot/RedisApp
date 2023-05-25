
// Replace with your cache's host, port, and access key.
using StackExchange.Redis;

string redisHost = "rediskj.redis.cache.windows.net";
int redisPort = 6380;
string redisKey = "UzGZq6L5savmMaUJk38AX4sxNE3AMCj4CAzCaNNClxg=";


// Create a Redis ConnectionMultiplexer instance.
var configOptions = new ConfigurationOptions
{
    EndPoints = { { redisHost, redisPort } },
    Password = redisKey,
    Ssl = true
};


//var value = await cache.StringGetAsync("1");

//Console.WriteLine(  $"My value is: {value}");

//var isOK =  await cache.StringSetAsync("1", DateTime.Now.ToString());

//var newValue = await cache.StringGetAsync("1");

//Console.WriteLine( $"My old value was: {value}\nMy new value is: {newValue}" );

int key = 1;

await Parallel.ForEachAsync(Enumerable.Range(0, 4), async (act, token) =>
{
   using var connection = await ConnectionMultiplexer.ConnectAsync(configOptions);

    // Get a reference to the Redis database.
    var cache = connection.GetDatabase();

    for (int i = 0; i < 1000; i++)
    {
        var value = await cache.StringGetAsync(key.ToString());
        await cache.StringSetAsync(key.ToString(), DateTime.Now.ToString());
        var isOK = await cache.StringSetAsync(key.ToString(), DateTime.Now.ToString());

        if (isOK)
            Console.WriteLine($"THRED: {act} From key: {key} My value is: {value}");



        /*Task.Delay(1000).Wait();*/
        key++;
    }

});

//await connection.CloseAsync();