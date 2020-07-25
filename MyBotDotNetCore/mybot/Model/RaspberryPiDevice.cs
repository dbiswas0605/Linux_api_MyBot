using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Gpio;
using System.Threading;

namespace Bot.Device.RaspberryPi
{
    internal class RaspberryPiDevice
    {
        // Get the default GPIO controller on the system
        const int PIN_MOTOR_LEFT_FORWARD = 27;
        const int PIN_MOTOR_LEFT_BACKWARD = 22;

        const int PIN_MOTOR_RIGHT_FORWARD = 23;
        const int PIN_MOTOR_RIGHT_BACKWARD = 24;

        GpioController leftwheelF, leftwheelB;
        GpioController rightwheelF, rightwheelB;

        public RaspberryPiDevice()
        {
            // Get the default GPIO controller on the system
            leftwheelF = new GpioController();
            leftwheelB = new GpioController();
            rightwheelF = new GpioController();
            rightwheelB = new GpioController();

            if (leftwheelF == null || leftwheelB == null || rightwheelF == null ||rightwheelB == null)
                return; // GPIO not available on this system

            #region set pins for output
            leftwheelF.OpenPin(PIN_MOTOR_LEFT_FORWARD, PinMode.Output);
            leftwheelB.OpenPin(PIN_MOTOR_LEFT_BACKWARD, PinMode.Output);

            rightwheelF.OpenPin(PIN_MOTOR_RIGHT_FORWARD, PinMode.Output);
            rightwheelB.OpenPin(PIN_MOTOR_RIGHT_BACKWARD, PinMode.Output);
            #endregion set pins for output

            #region set pins to low voltage
            leftwheelF.Write(PIN_MOTOR_LEFT_FORWARD, PinValue.Low);
            leftwheelB.Write(PIN_MOTOR_LEFT_BACKWARD, PinValue.Low);

            rightwheelF.Write(PIN_MOTOR_RIGHT_FORWARD, PinValue.Low);
            rightwheelB.Write(PIN_MOTOR_RIGHT_BACKWARD, PinValue.Low);
            #endregion set pins to low voltage
        }

        private void leftWheelGoForward()
        {
            leftwheelF.Write(PIN_MOTOR_LEFT_FORWARD, PinValue.High);
            leftwheelB.Write(PIN_MOTOR_LEFT_BACKWARD, PinValue.Low);
        }

        private void rightWheelGoForward()
        {
            rightwheelF.Write(PIN_MOTOR_RIGHT_FORWARD, PinValue.High);
            rightwheelB.Write(PIN_MOTOR_RIGHT_BACKWARD, PinValue.Low);
        }

        private void leftWheelGoBackward()
        {
            leftwheelF.Write(PIN_MOTOR_LEFT_FORWARD, PinValue.Low);
            leftwheelB.Write(PIN_MOTOR_LEFT_BACKWARD, PinValue.High);            
        }

        private void rightWheelGoBackward()
        {
            leftwheelF.Write(PIN_MOTOR_LEFT_FORWARD, PinValue.Low);
            leftwheelB.Write(PIN_MOTOR_LEFT_BACKWARD, PinValue.High);
        }

        private void leftWheelStop()
        {
            leftwheelF.Write(PIN_MOTOR_LEFT_FORWARD, PinValue.Low);
            leftwheelB.Write(PIN_MOTOR_LEFT_BACKWARD, PinValue.Low);
        }

        private void rightWheelStop()
        {
            rightwheelF.Write(PIN_MOTOR_RIGHT_FORWARD, PinValue.Low);
            rightwheelB.Write(PIN_MOTOR_RIGHT_BACKWARD, PinValue.Low);
        }

        public void exit()
        {
            leftwheelF.Write(PIN_MOTOR_LEFT_FORWARD, PinValue.Low);
            leftwheelB.Write(PIN_MOTOR_LEFT_BACKWARD, PinValue.Low);

            leftwheelB.Dispose();
            leftwheelF.Dispose();

            rightwheelF.Write(PIN_MOTOR_RIGHT_FORWARD, PinValue.Low);
            rightwheelB.Write(PIN_MOTOR_RIGHT_BACKWARD, PinValue.Low);

            rightwheelB.Dispose();
            rightwheelF.Dispose();
        }

        public void moveBackward()
        {
            leftWheelGoForward();
            rightWheelGoForward();
        }

        public void moveForward()
        {
            leftWheelGoBackward();
            rightWheelGoBackward();
        }

        public void stop()
        {
            rightWheelStop();
            leftWheelStop();
        }

        public void turnLeft()
        {
            stop();

            rightWheelGoForward();
            Thread.Sleep(2000);
            leftWheelGoBackward();
            Thread.Sleep(2000);

            stop();
        }

        public void turnRight()
        {
            stop();

            rightWheelGoBackward();
            Thread.Sleep(2000);
            leftWheelGoForward();
            Thread.Sleep(2000);

            stop();
        }
    }
}
