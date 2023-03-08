using FluentAssertions;

using NetArchTest.Rules;

using Words.Domain;

namespace Words.Test.Architecture;

public class ArchitectureTest {
    private const string DomainNameSpace = "Words.Domain";
    private const string ApplicationNameSpace = "Words.Application";
    private const string InfastructureNameSpace = "Words.Infastructure";
    private const string PresentationNameSpace = "Words.Presentation";

    [Fact]
    public void Domain_Should_Not_Have_DependencyOnOtherProjects() {
        var assembly = typeof(Words.Domain.AssemblyReference).Assembly;

        var otherProjects = new[] {
            ApplicationNameSpace,
            InfastructureNameSpace,
        };

        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_Should_Not_Have_DependencyOnOtherProjects() {
        var assembly = typeof(Words.Application.AssemblyReference).Assembly;

        var otherProjects = new[] {
            InfastructureNameSpace,
        };

        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infastructure_Should_Not_Have_DependencyOnOtherProjects() {
        var assembly = typeof(Words.Infastructure.AssemblyReference).Assembly;

        var otherProjects = new[] {
            PresentationNameSpace,
        };

        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }
}
