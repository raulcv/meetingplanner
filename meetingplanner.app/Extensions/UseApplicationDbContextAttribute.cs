using System.Reflection;
using meetingplanner.app.Data;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;

namespace meetingplanner.app
{
  public class UseApplicationDbContextAttribute : ObjectFieldDescriptorAttribute
  {
    /* It have an error
    public override void OnConfigure(IDescriptorContext context, IObjectFieldDescriptor descriptor, MemberInfo member)
    {
      descriptor.UseDbContext<AppDbContext>();
    }*/
    protected override void OnConfigure(IDescriptorContext context, IObjectFieldDescriptor descriptor, MemberInfo member)
    {
      descriptor.UseDbContext<AppDbContext>();
    }
  }
  
}