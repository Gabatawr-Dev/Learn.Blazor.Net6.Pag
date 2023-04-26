using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Learn.Blazor.Net6.Pag.Server.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Price", "Title" },
                values: new object[,]
                {
                    { new Guid("00000000-1000-0000-0000-000000000000"), "The Great Gatsby is a 1925 novel written by American author F. Scott Fitzgerald that follows a cast of characters living in the fictional town of West Egg on prosperous Long Island in the summer of 1922.", "https://fastly.picsum.photos/id/765/200/300.jpg?hmac=yRNlm1EXdqLX1q3pNM20VW3eARvw3XIoph6gf4qydTk", 9.99m, "The Great Gatsby" },
                    { new Guid("00000000-1000-1000-0000-000000000000"), "A classic novel by Jane Austen that explores themes of love, marriage, and societal expectations.", "https://fastly.picsum.photos/id/381/200/300.jpg?hmac=DHcGsLBoQPJC-_rudxS4AdZuSE9UoOFP2U2v2veUAok", 7.99m, "Pride and Prejudice" },
                    { new Guid("00000000-1000-2000-0000-000000000000"), "A novel by Gabriel Garcia Marquez that tells the story of the Buendia family over seven generations and explores themes of love, magic, and the cyclical nature of time.", "https://fastly.picsum.photos/id/374/200/300.jpg?hmac=O7_6jZztETgk8S2eFcdlCNlqe50qS5u-OW5hs-EoNMo", 11.99m, "One Hundred Years of Solitude" },
                    { new Guid("00000000-2000-0000-0000-000000000000"), "The Catcher in the Rye is a 1951 novel by J. D. Salinger. A controversial novel originally published for adults, it has since become popular with adolescent readers for its themes of teenage angst and alienation.", "https://fastly.picsum.photos/id/906/200/300.jpg?hmac=7sarKOMVDlgOBTc6eUDUf0M4S-M-4jF0X0uix_sMALU", 8.99m, "The Catcher in the Rye" },
                    { new Guid("00000000-3000-0000-0000-000000000000"), "The Grapes of Wrath is an American realist novel written by John Steinbeck and published in 1939. The book won the National Book Award and Pulitzer Prize for Fiction, and it was cited prominently when Steinbeck was awarded the Nobel Prize in 1962.", "https://fastly.picsum.photos/id/688/200/300.jpg?hmac=6_iDeSdl4f6R2Lre1xFrJ9VaO8OQHMJD_PL5lEypBGI", 7.99m, "The Grapes of Wrath" },
                    { new Guid("00000000-4000-0000-0000-000000000000"), "The Lord of the Rings is an epic high fantasy novel written by English author and scholar J. R. R. Tolkien. The story began as a sequel to Tolkien's 1937 fantasy novel The Hobbit, but eventually developed into a much larger work.", "https://fastly.picsum.photos/id/448/200/300.jpg?hmac=9a1pqR60H2xWN80jPWfmdVkRII-wEQZceiSHpJSZnE4", 6.99m, "The Lord of the Rings" },
                    { new Guid("00000000-5000-0000-0000-000000000000"), "The Adventures of Huckleberry Finn is a novel by Mark Twain, first published in the United Kingdom in December 1884 and in the United States in February 1885. Commonly named among the Great American Novels, the work is among the first in major American literature to be written throughout in vernacular English, characterized by local color regionalism.", "https://fastly.picsum.photos/id/602/200/300.jpg?hmac=TkzlF12MtJomcmqzsOc-CR43gSl3xnotDQRPBvM7Avw", 5.99m, "The Adventures of Huckleberry Finn" },
                    { new Guid("00000000-6000-0000-0000-000000000000"), "The Adventures of Tom Sawyer is a novel by Mark Twain, first published in the United Kingdom in December 1884 and in the United States in February 1885. Commonly named among the Great American Novels, the work is among the first in major American literature to be written throughout in vernacular English, characterized by local color regionalism.", "https://fastly.picsum.photos/id/555/200/300.jpg?hmac=HbW_j1WvVDr5eTwXP2bsohZEiyBe-G6fsPkgAxJe9ps", 4.99m, "The Adventures of Tom Sawyer" },
                    { new Guid("00000000-7000-0000-0000-000000000000"), "The Adventures of Sherlock Holmes is a collection of twelve stories by Arthur Conan Doyle, featuring his fictional detective Sherlock Holmes. It was first published on 14 October 1892; the individual stories had been serialised in The Strand Magazine between July 1891 and June 1892.", "https://fastly.picsum.photos/id/831/200/300.jpg?hmac=IC6dJVWWVnJ-extXtn0D9QDwKwbQ-tA_M6UD2T9zUbQ", 3.99m, "The Adventures of Sherlock Holmes" },
                    { new Guid("00000000-8000-0000-0000-000000000000"), "A Pulitzer Prize-winning novel by Harper Lee set in the Deep South and deals with issues of racism and injustice.", "https://fastly.picsum.photos/id/530/200/300.jpg?hmac=pl2pzOmYOiMa6E_Ddf_SFQVGjDvmZ1xgj-JznVHuUsg", 12.99m, "To Kill a Mockingbird" },
                    { new Guid("00000000-9000-0000-0000-000000000000"), "A dystopian novel by George Orwell that depicts a totalitarian society and explores themes of government surveillance, censorship, and propaganda.", "https://fastly.picsum.photos/id/581/200/300.jpg?hmac=Xsg_aDXsNDPBGUvQPMKuMn2f4XS6zkrgh0vnl2lzljk", 8.99m, "1984" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
