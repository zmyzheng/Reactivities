using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class Create
    {


        public class Command : IRequest
        {
            public Guid Id { get; set; }

            [Required]  // 以annotation的方式验证
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime Date { get; set; }
            public string Category { get; set; }
            public string City { get; set; }
            public string Venue { get; set; }
        }

        // 以fluent middleware的方式验证
        public class ComandValidator : AbstractValidator<Command>
        {
            public ComandValidator()
            {
                RuleFor(x => x.Description).NotEmpty();
            }
        }

        // // 接受Query，没有返回值（实际上因为下面用了Unit，所以返回空object
        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                this._userAccessor = userAccessor;
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

                var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == _userAccessor.GetCurrentUsername());
                var attendee = new UserActivity
                {
                    AppUser = user,
                    Activity = activity,
                    IsHost = true,
                    DateJoined = DateTime.Now
                };
                _context.UserActivities.Add(attendee);


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