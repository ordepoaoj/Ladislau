using Microsoft.AspNetCore.Identity;

namespace Web20.Areas.Identity
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresNonAlphanumeric() { return new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "A senha deve ter pelo menos um caracter especial." }; }
        public override IdentityError PasswordRequiresDigit() { return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = "A senha deve ter pelo menos um número. ('0'-'9')." }; }
        public override IdentityError PasswordRequiresLower() { return new IdentityError { Code = nameof(PasswordRequiresLower), Description = "A senha deve ter pelo menos uma letra minuscula. ('a'-'z')." }; }
        public override IdentityError PasswordRequiresUpper() { return new IdentityError { Code = nameof(PasswordRequiresUpper), Description = "A senha deve ter pelo menos uma letra maiuscula. ('A'-'Z')." }; }
    }
}
