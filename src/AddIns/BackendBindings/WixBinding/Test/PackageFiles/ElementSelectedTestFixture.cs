﻿// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Matthew Ward" email="mrward@users.sourceforge.net"/>
//     <version>$Revision$</version>
// </file>

using ICSharpCode.SharpDevelop.Project;
using ICSharpCode.WixBinding;
using NUnit.Framework;
using System;
using System.IO;
using System.Xml;
using WixBinding.Tests.Utils;

namespace WixBinding.Tests.PackageFiles
{
	/// <summary>
	/// The package item (e.g. directory) is selected in the view.
	/// </summary>
	[TestFixture]
	public class ElementSelectedTestFixture : PackageFilesTestFixtureBase
	{
		WixXmlAttribute idAttribute;
		
		[TestFixtureSetUp]
		public void SetUpFixture()
		{
			base.InitFixture();
			XmlElement rootDirectoryElement = editor.Document.RootDirectory;
			XmlElement directoryElement = (XmlElement)rootDirectoryElement.ChildNodes[0];
			view.SelectedElement = directoryElement;
			editor.SelectedElementChanged();
			idAttribute = base.GetAttribute(view.Attributes, "Id");
		}
		
		[Test]
		public void SelectedElementAccessed()
		{
			Assert.IsTrue(view.SelectedElementAccessed);
		}
		
		[Test]
		public void IdAttributeValue()
		{
			Assert.AreEqual("ProgramFilesFolder", idAttribute.Value);
		}
		
		[Test]
		public void IdAttributeType()
		{
			Assert.AreEqual(WixXmlAttributeType.Text, idAttribute.AttributeType);
		}
	
		/// <summary>
		/// Make sure that unspecified attributes are added.
		/// </summary>
		[Test]
		public void UnspecifiedFileSourceAttributeExists()
		{
			Assert.AreEqual(String.Empty, base.GetAttribute(view.Attributes, "FileSource").Value);
		}
		
		[Test]
		public void IsDirty()
		{
			Assert.IsFalse(view.IsDirty);
		}
		
		[Test]
		public void AttributesChangedCalled()
		{
			Assert.IsTrue(view.IsAttributesChangedCalled);
		}
		
		protected override string GetWixXml()
		{
			return "<Wix xmlns=\"http://schemas.microsoft.com/wix/2006/wi\">\r\n" +
				"\t<Product Name=\"MySetup\" \r\n" +
				"\t         Manufacturer=\"\" \r\n" +
				"\t         Id=\"F4A71A3A-C271-4BE8-B72C-F47CC956B3AA\" \r\n" +
				"\t         Language=\"1033\" \r\n" +
				"\t         Version=\"1.0.0.0\">\r\n" +
				"\t\t<Package Id=\"6B8BE64F-3768-49CA-8BC2-86A76424DFE9\"/>\r\n" +
				"\t\t<Directory Id=\"TARGETDIR\" SourceName=\"SourceDir\">\r\n" +
				"\t\t\t<Directory Id=\"ProgramFilesFolder\" Name=\"PFiles\">\r\n" +
				"\t\t\t\t<Directory Id=\"MyCompany\" Name=\"MyComp\">\r\n" +
				"\t\t\t\t\t<Directory Id=\"INSTALLDIR\" Name=\"MyApp\">\r\n" +
				"\t\t\t\t\t</Directory>\r\n" +
				"\t\t\t\t</Directory>\r\n" +
				"\t\t\t</Directory>\r\n" +
				"\t\t</Directory>\r\n" +
				"\t</Product>\r\n" +
				"</Wix>";
		}
	}
}
