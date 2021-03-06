// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 4.61
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace CustomLogic
{

    // AomFieldObject
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.27.0.0")]
    public partial class AomFieldObjectMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<AomFieldObject>
    {
        public AomFieldObjectMap()
            : this("dbo")
        {
        }

        public AomFieldObjectMap(string schema)
        {
            ToTable("AomFieldObject", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"ID").HasColumnType("uniqueidentifier").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.AomObjectId).HasColumnName(@"AomObjectId").HasColumnType("uniqueidentifier").IsRequired();
            Property(x => x.AomFieldMetaId).HasColumnName(@"AomFieldMetaId").HasColumnType("uniqueidentifier").IsRequired();
            Property(x => x.Value).HasColumnName(@"Value").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(250);

            // Foreign keys
            HasRequired(a => a.AomFieldMeta).WithMany(b => b.AomFieldObjects).HasForeignKey(c => c.AomFieldMetaId).WillCascadeOnDelete(false); // FK_dbo.AomFieldObject_dbo.AomFieldMeta_AomFieldMetaId
            HasRequired(a => a.AomObject).WithMany(b => b.AomFieldObjects).HasForeignKey(c => c.AomObjectId); // FK_dbo.AomFieldObject_dbo.AomObject_AomObjectId
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
