using MediatR;
using System;

public class BarCommand : ICommand<ICommandResult>
{ 
    public Guid Id { get; set; } 
}

public class MyOtherDataQuery : IQuery<IList<Data>>
{
    public Guid BarId { get; set; }
    public MyOtherDataQuery(Guid barId)
    {
        BarId = barId;
    }
}

public class FooService : ICommandHandler<BarCommand>
{
    private readonly IMediator _mediator;
  
    public FooService(IMediator mediator)
    {
        _mediator = mediator ?? Throw new ArgumentNullException(nameof(mediator));
    }
    
    public ICommandResult Handle(BarCommand command)
    {
        var otherDataNeeded = _mediator.Send(new MyOtherDataQuery(command.Id));
        return new CommandResult(otherDataNeeded);
    }
}

public class MyOtherDataRepository : IQueryHandler<MyOtherDataQuery<IList<Data>>
{
    public IList<Data> Handle(MyOtherDataQuery query)
    {
        return new List<Data>() { 
            new Data { Id = query.BarId } 
        };
    }
}
