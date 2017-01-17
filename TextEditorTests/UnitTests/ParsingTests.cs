using Example.TextEditor.Model.SystemIO;
using Example.TextEditor.ViewModel.Document;
using Example.TextEditor.ViewModel.Parsing;
using Example.TextEditor.ViewModel.Parsing.Elements;
using Example.TextEditor.ViewModel.Parsing.XML;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TextEditorTests.UnitTests
{
	[TestClass]
	public class ParsingTests
	{
		[TestMethod]
		[TestCategory("Parsing")]
		public void WhenADocumentChangesIsText_ItShouldUpdateIsParsingStructure()
		{
			string validPlainText = "Example of plain text";
			Mock<IParserViewModel> parserMock = new Mock<IParserViewModel>();
			DocumentViewModel document = new DocumentViewModel(Mock.Of<ISystemIOFacade>(), parserMock.Object);

			//Act
			document.Text = validPlainText;

			//Assert
			parserMock.Verify(obj => obj.UpdateStructure(validPlainText), Times.Once);
		}

		[TestMethod]
		[TestCategory("Parsing")]
		public void WhenPareserReceivedPlainText_ItShoulReturnNull()
		{
			string validPlainText = "Example of plain text";
			IParserViewModel parser = new XMLParserViewModel();
			DocumentViewModel document = new DocumentViewModel(Mock.Of<ISystemIOFacade>(), parser);

			//Act
			document.Text = validPlainText;

			//Assert
			Assert.IsNull(document.Elements);
		}

		[TestMethod]
		[TestCategory("Parsing")]
		public void WhenPareserReceivedGoodXmlText_ItShoulNotReturnNull()
		{
			string validXMLText = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Root/>";
			IParserViewModel parser = new XMLParserViewModel();
			DocumentViewModel document = new DocumentViewModel(Mock.Of<ISystemIOFacade>(), parser);

			//Act
			document.Text = validXMLText;

			//Assert
			Assert.IsInstanceOfType(document.Elements, typeof(RootVM));
		}

		[TestMethod]
		[TestCategory("Parsing")]
		public void WhenPareserReceivedGoodXmlText_ItShoulReturnACorrectStructure()
		{
			string xmlText =
				"<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
				"<APR>" +
				"	<Configuration>" +
				"		<LanguajeFile>APRComponent_FR.xml</LanguajeFile>" +
				"		<EnablePassword>False</EnablePassword>" +
				"		<Password>2255</Password>" +
				"		<EnableLists>true</EnableLists>" +
				"		<EnableEdition>true</EnableEdition>" +
				"		<EnableDetails>true</EnableDetails>" +
				"		<EnableConfigGUI>true</EnableConfigGUI>" +
				"		<EnableGrid>False</EnableGrid>" +
				"		<EnableKeyboard>False</EnableKeyboard>" +
				"		<EnableJournal>False</EnableJournal>" +
				"		<APRFileName>VeterinaryWithoutHorse.apr</APRFileName>" +
				"		<APRSkinFile>VeterinaryHDSkin</APRSkinFile>" +
				"		<IPHub>192.168.1.1</IPHub>" +
				"		<PortIPHub>2020</PortIPHub>" +
				"	</Configuration>" +
				"</APR>";

			IParserViewModel parser = new XMLParserViewModel();
			DocumentViewModel document = new DocumentViewModel(Mock.Of<ISystemIOFacade>(), parser);

			//Act
			document.Text = xmlText;

			//Assert
			Assert.IsInstanceOfType(document.Elements, typeof(RootVM));
		}
	}
}
