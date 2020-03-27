using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Create
    {


        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime Date { get; set; }
            public string Category { get; set; }
            public string City { get; set; }
            public string Venue { get; set; }
        }

        // // 接受Query，没有返回值（实际上因为下面用了Unit，所以返回空object
        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                this._context = context;
            }

            // 虽然返回值为void，但这里依然写Unit
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = new Activity
                {
                    Id = request.Id,
                    Title = request.Title,
                    Description = request.Description,
                    Category = request.Category,
                    Date = request.Date,
                    City = request.City,
                    Venue = request.Venue
                };

                _context.Activities.Add(activity);
                var success = await _context.SaveChangesAsync() > 0;
                if (success) 
                {
                    return Unit.Value; // this is just an empty object
                }
                throw new Exception("Problem saving changes");
            }
        }
    }
}