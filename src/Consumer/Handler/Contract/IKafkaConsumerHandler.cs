using System;

namespace GarageGroup.Infra;

public interface IKafkaConsumerHandler : IHandler<Unit, Unit>;