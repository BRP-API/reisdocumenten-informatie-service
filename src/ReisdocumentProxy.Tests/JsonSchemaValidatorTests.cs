using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReisdocumentProxy.Tests
{
    public class JsonSchemaValidatorTests
    {
        const string ReisdocumentenQuerySchema = @"{
            'required' : [ 'fields', 'gemeenteVanInschrijving', 'type' ],
            'type' : 'object',
            'properties' : {
                'type' : {
                    'pattern': '^RaadpleegMetReisdocumentnummer$',
                    'type' : 'string'
                },
                'fields' : {
                    'maxItems' : 25,
                    'minItems' : 1,
                    'type' : 'array',
                    'items' : {
                        'pattern' : '^[a-zA-Z0-9\\._]{1,200}$',
                        'type' : 'string'
                    }
                },
                'gemeenteVanInschrijving' : {
                    'pattern' : '^[0-9]{4}$',
                    'type' : 'string'
                }
            },
            'discriminator' : {
                'propertyName' : 'type',
                'mapping' : {
                    'RaadpleegMetReisdocumentnummer' : '#/components/schemas/RaadpleegMetReisdocumentnummer',
                    'ZoekMetBurgerservicenummer' : '#/components/schemas/ZoekMetBurgerservicenummer'
                }
            }
        }";

        [Fact]
        public void GeenParametersOpgegeven()
        {
            var json = "{}";

            JSchema jsonSchema = JSchema.Parse(ReisdocumentenQuerySchema);
            JToken jtoken = JToken.Parse(json);

            jtoken.IsValid(jsonSchema, out IList<ValidationError> errors).Should().BeFalse();

            errors.Count.Should().Be(1);

            errors[0].ErrorType.Should().Be(ErrorType.Required);
            errors[0].Message.Should().Be("Required properties are missing from object: fields, gemeenteVanInschrijving, type.");
        }

        [Fact]
        public void TypeParameterIsNietOpgegeven()
        {
            var json = @"{
                'fields': ['foo'],
                'gemeenteVanInschrijving': '1234'
            }";

            JSchema jsonSchema = JSchema.Parse(ReisdocumentenQuerySchema);
            JToken jtoken = JToken.Parse(json);

            jtoken.IsValid(jsonSchema, out IList<ValidationError> errors).Should().BeFalse();

            errors.Count.Should().Be(1);

            errors[0].ErrorType.Should().Be(ErrorType.Required);
            errors[0].Message.Should().Be("Required properties are missing from object: type.");
        }

        [Fact]
        public void TypeParameterBevatGeenWaarde()
        {
            var json = @"{
                'type': '',
                'fields': ['foo'],
                'gemeenteVanInschrijving': '1234'
            }";

            JSchema jsonSchema = JSchema.Parse(ReisdocumentenQuerySchema);
            JToken jtoken = JToken.Parse(json);

            jtoken.IsValid(jsonSchema, out IList<ValidationError> errors).Should().BeFalse();

            errors.Count.Should().Be(1);

            errors[0].ErrorType.Should().Be(ErrorType.Pattern);
            errors[0].Message.Should().Be("String '' does not match regex pattern '^RaadpleegMetReisdocumentnummer$'.");
        }

        [Fact]
        public void TypeParameterBevatOngeldigeWaarde()
        {
            var json = @"{
                'type': 'ZoekMetReisdocumentnummer',
                'fields': ['foo'],
                'gemeenteVanInschrijving': '1234'
            }";

            JSchema jsonSchema = JSchema.Parse(ReisdocumentenQuerySchema);
            JToken jtoken = JToken.Parse(json);

            jtoken.IsValid(jsonSchema, out IList<ValidationError> errors).Should().BeFalse();

            errors.Count.Should().Be(1);

            errors[0].ErrorType.Should().Be(ErrorType.Pattern);
            errors[0].Message.Should().Be("String 'ZoekMetReisdocumentnummer' does not match regex pattern '^RaadpleegMetReisdocumentnummer$'.");
        }

        // [Fact]
        // public void JsonIsOngeldig()
        // {
        //     var json = @"{
        //         'type': 'RaadpleegMetReisdocumentnummer',
        //         'fields': ['foo'],
        //         'gemeenteVanInschrijving': '1234'
        //     }";

        //     JSchema jsonSchema = JSchema.Parse(ReisdocumentenQuerySchema);
        //     JToken jtoken = JToken.Parse(json);

        //     jtoken.IsValid(jsonSchema, out IList<ValidationError> errors).Should().BeFalse();

        //     errors.Count.Should().Be(1);

        //     errors[0].ErrorType.Should().Be(ErrorType.Pattern);
        //     errors[0].Message.Should().Be("String 'ZoekMetReisdocumentnummer' does not match regex pattern '^RaadpleegMetReisdocumentnummer$'.");
        // }
    }
}
