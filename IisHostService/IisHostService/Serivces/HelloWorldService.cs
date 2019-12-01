using IisHostService.Serivces.Contracts;

namespace IisHostService.Serivces
{
    public class HelloWorldService: IHelloWorldService
    {
        public string GetHelloWorld => "Hello World!";
    }
}
