using Microsoft.AspNetCore.Mvc;
using Bot.Device.RaspberryPi;

namespace mybot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignalController : ControllerBase
    {
        RaspberryPiDevice device = new RaspberryPiDevice();

        [HttpGet]
        [Route("TurnRight")]
        public void TurnRight()
        {
            device.turnRight();
        }

        [HttpGet]
        [Route("TurnLeft")]
        public void TurnLeft()
        {
            device.turnLeft();
        }

        [HttpGet]
        [Route("MoveForward")]
        public void MoveForward()
        {
            device.moveForward();
        }

        [HttpGet]
        [Route("MoveBackward")]
        public void MoveBackward()
        {
            device.moveBackward();
        }                

        [HttpGet]
        [Route("Stop")]
        public void Stop()
        {
            device.stop();
        }  

        [HttpGet]
        [Route("Exit")]
        public void Exit()
        {
            device.exit();
        }  
    }
}
