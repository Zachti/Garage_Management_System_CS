namespace Garage {

    internal class ElectricMotorCycleFactory : MotorCycleFactory {
        protected sealed override float MaxEnergy => 2.5f;

        protected sealed override Engine getEngineData() => new ElectricEngine(MaxEnergy);
    }
}