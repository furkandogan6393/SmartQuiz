using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using WebAPI.DataAccess.Abstract;
using WebAPI.DataAccess.Concrete;
using WebAPI.Services.Abstract;
using WebAPI.Services.Concrete;

namespace WebAPI.DependencyResolvers
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EfUserDal>().As<IUserDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfQuizDal>().As<IQuizDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfQuestionDal>().As<IQuestionDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfAnswerDal>().As<IAnswerDal>().InstancePerLifetimeScope();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>().InstancePerLifetimeScope();
            builder.RegisterType<UserManager>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<AuthManager>().As<IAuthService>().InstancePerLifetimeScope();
            builder.RegisterType<QuizManager>().As<IQuizService>().InstancePerLifetimeScope();
            builder.RegisterType<QuestionManager>().As<IQuestionService>().InstancePerLifetimeScope();
            builder.RegisterType<AnswerManager>().As<IAnswerService>().InstancePerLifetimeScope();
            builder.RegisterType<EfDocumentDal>().As<IDocumentDal>().InstancePerLifetimeScope();
            builder.RegisterType<DocumentManager>().As<IDocumentService>().InstancePerLifetimeScope();
            builder.RegisterType<AIManager>().As<IAIService>().InstancePerLifetimeScope();
            builder.RegisterType<EfTenantDal>().As<ITenantDal>().InstancePerLifetimeScope();
            builder.RegisterType<TenantManager>().As<ITenantService>().InstancePerLifetimeScope();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
