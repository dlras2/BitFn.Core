using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace BitFn.Core.Extensions
{
	/// <summary>
	///     Extension methods for the <see cref="string" /> class.
	/// </summary>
	public static class ForString
	{
		private static readonly IDictionary<char, string> UnicodeToAscii = new Dictionary<char, string>
		{
			// Common hand-identified letters
			['\u00E6'] = "ae", // æ — latin small letter ae
			['\u00C6'] = "AE", // Æ — latin capital letter ae
			['\u0153'] = "oe", // œ — latin small ligature oe
			['\u0152'] = "OE", // Œ — latin capital ligature oe
			['\u00DF'] = "ss", // ß — german small eszett
			['\u1E9E'] = "SS", // ẞ — german capital eszett

			// Programatically identified Latin letters
			['\u1DF2'] = "a", // ᷲ — combining latin small letter a with diaeresis
			['\uFF41'] = "a", // ａ — fullwidth latin small letter a
			['\uAB31'] = "a", // ꬱ — latin small letter a reversed-schwa
			['\u1D8F'] = "a", // ᶏ — latin small letter a with retroflex hook
			['\u1E9A'] = "a", // ẚ — latin small letter a with right half ring
			['\u2C65'] = "a", // ⱥ — latin small letter a with stroke
			['\u2090'] = "a", // ₐ — latin subscript small letter a
			['\uFF21'] = "A", // Ａ — fullwidth latin capital letter a
			['\u023A'] = "A", // Ⱥ — latin capital letter a with stroke
			['\u1DE8'] = "b", // ᷨ — combining latin small letter b
			['\uFF42'] = "b", // ｂ — fullwidth latin small letter b
			['\uA797'] = "b", // ꞗ — latin small letter b with flourish
			['\u0253'] = "b", // ɓ — latin small letter b with hook
			['\u1D6C'] = "b", // ᵬ — latin small letter b with middle tilde
			['\u1D80'] = "b", // ᶀ — latin small letter b with palatal hook
			['\u0180'] = "b", // ƀ — latin small letter b with stroke
			['\u0183'] = "b", // ƃ — latin small letter b with topbar
			['\uFF22'] = "B", // Ｂ — fullwidth latin capital letter b
			['\uA796'] = "B", // Ꞗ — latin capital letter b with flourish
			['\u0181'] = "B", // Ɓ — latin capital letter b with hook
			['\u0243'] = "B", // Ƀ — latin capital letter b with stroke
			['\u0182'] = "B", // Ƃ — latin capital letter b with topbar
			['\uFF43'] = "c", // ｃ — fullwidth latin small letter c
			['\uA793'] = "c", // ꞓ — latin small letter c with bar
			['\u0255'] = "c", // ɕ — latin small letter c with curl
			['\u0188'] = "c", // ƈ — latin small letter c with hook
			['\uA794'] = "c", // ꞔ — latin small letter c with palatal hook
			['\u023C'] = "c", // ȼ — latin small letter c with stroke
			['\uFF23'] = "C", // Ｃ — fullwidth latin capital letter c
			['\uA792'] = "C", // Ꞓ — latin capital letter c with bar
			['\u0187'] = "C", // Ƈ — latin capital letter c with hook
			['\u023B'] = "C", // Ȼ — latin capital letter c with stroke
			['\uFF44'] = "d", // ｄ — fullwidth latin small letter d
			['\u0221'] = "d", // ȡ — latin small letter d with curl
			['\u0257'] = "d", // ɗ — latin small letter d with hook
			['\u1D91'] = "d", // ᶑ — latin small letter d with hook and tail
			['\u1D6D'] = "d", // ᵭ — latin small letter d with middle tilde
			['\u1D81'] = "d", // ᶁ — latin small letter d with palatal hook
			['\u0111'] = "d", // đ — latin small letter d with stroke
			['\u0256'] = "d", // ɖ — latin small letter d with tail
			['\u018C'] = "d", // ƌ — latin small letter d with topbar
			['\uFF24'] = "D", // Ｄ — fullwidth latin capital letter d
			['\u018A'] = "D", // Ɗ — latin capital letter d with hook
			['\u0110'] = "D", // Đ — latin capital letter d with stroke
			['\u018B'] = "D", // Ƌ — latin capital letter d with topbar
			['\uFF45'] = "e", // ｅ — fullwidth latin small letter e
			['\uAB34'] = "e", // ꬴ — latin small letter e with flourish
			['\u2C78'] = "e", // ⱸ — latin small letter e with notch
			['\u1D92'] = "e", // ᶒ — latin small letter e with retroflex hook
			['\u0247'] = "e", // ɇ — latin small letter e with stroke
			['\u2091'] = "e", // ₑ — latin subscript small letter e
			['\uFF25'] = "E", // Ｅ — fullwidth latin capital letter e
			['\u0246'] = "E", // Ɇ — latin capital letter e with stroke
			['\u1DEB'] = "f", // ᷫ — combining latin small letter f
			['\uFF46'] = "f", // ｆ — fullwidth latin small letter f
			['\u0192'] = "f", // ƒ — latin small letter f with hook
			['\u1D6E'] = "f", // ᵮ — latin small letter f with middle tilde
			['\u1D82'] = "f", // ᶂ — latin small letter f with palatal hook
			['\uA799'] = "f", // ꞙ — latin small letter f with stroke
			['\uFF26'] = "F", // Ｆ — fullwidth latin capital letter f
			['\u0191'] = "F", // Ƒ — latin capital letter f with hook
			['\uA798'] = "F", // Ꞙ — latin capital letter f with stroke
			['\uFF47'] = "g", // ｇ — fullwidth latin small letter g
			['\u0260'] = "g", // ɠ — latin small letter g with hook
			['\uA7A1'] = "g", // ꞡ — latin small letter g with oblique stroke
			['\u1D83'] = "g", // ᶃ — latin small letter g with palatal hook
			['\u01E5'] = "g", // ǥ — latin small letter g with stroke
			['\uFF27'] = "G", // Ｇ — fullwidth latin capital letter g
			['\u0193'] = "G", // Ɠ — latin capital letter g with hook
			['\uA7A0'] = "G", // Ꞡ — latin capital letter g with oblique stroke
			['\u01E4'] = "G", // Ǥ — latin capital letter g with stroke
			['\uFF48'] = "h", // ｈ — fullwidth latin small letter h
			['\u2C68'] = "h", // ⱨ — latin small letter h with descender
			['\u0266'] = "h", // ɦ — latin small letter h with hook
			['\uA795'] = "h", // ꞕ — latin small letter h with palatal hook
			['\u0127'] = "h", // ħ — latin small letter h with stroke
			['\u2095'] = "h", // ₕ — latin subscript small letter h
			['\uFF28'] = "H", // Ｈ — fullwidth latin capital letter h
			['\u2C67'] = "H", // Ⱨ — latin capital letter h with descender
			['\uA7AA'] = "H", // Ɦ — latin capital letter h with hook
			['\u0126'] = "H", // Ħ — latin capital letter h with stroke
			['\uFF49'] = "i", // ｉ — fullwidth latin small letter i
			['\u1D7B'] = "i", // ᵻ — latin small capital letter i with stroke
			['\u1D96'] = "i", // ᶖ — latin small letter i with retroflex hook
			['\u0268'] = "i", // ɨ — latin small letter i with stroke
			['\u1D62'] = "i", // ᵢ — latin subscript small letter i
			['\u2071'] = "i", // ⁱ — superscript latin small letter i
			['\uFF29'] = "I", // Ｉ — fullwidth latin capital letter i
			['\u0197'] = "I", // Ɨ — latin capital letter i with stroke
			['\uA7FE'] = "I", // ꟾ — latin epigraphic letter i longa
			['\uFF4A'] = "j", // ｊ — fullwidth latin small letter j
			['\u029D'] = "j", // ʝ — latin small letter j with crossed-tail
			['\u0249'] = "j", // ɉ — latin small letter j with stroke
			['\u2C7C'] = "j", // ⱼ — latin subscript small letter j
			['\uFF2A'] = "J", // Ｊ — fullwidth latin capital letter j
			['\uA7B2'] = "J", // Ʝ — latin capital letter j with crossed-tail
			['\u0248'] = "J", // Ɉ — latin capital letter j with stroke
			['\uFF4B'] = "k", // ｋ — fullwidth latin small letter k
			['\u2C6A'] = "k", // ⱪ — latin small letter k with descender
			['\uA743'] = "k", // ꝃ — latin small letter k with diagonal stroke
			['\u0199'] = "k", // ƙ — latin small letter k with hook
			['\uA7A3'] = "k", // ꞣ — latin small letter k with oblique stroke
			['\u1D84'] = "k", // ᶄ — latin small letter k with palatal hook
			['\uA741'] = "k", // ꝁ — latin small letter k with stroke
			['\uA745'] = "k", // ꝅ — latin small letter k with stroke and diagonal stroke
			['\u2096'] = "k", // ₖ — latin subscript small letter k
			['\uFF2B'] = "K", // Ｋ — fullwidth latin capital letter k
			['\u2C69'] = "K", // Ⱪ — latin capital letter k with descender
			['\uA742'] = "K", // Ꝃ — latin capital letter k with diagonal stroke
			['\u0198'] = "K", // Ƙ — latin capital letter k with hook
			['\uA7A2'] = "K", // Ꞣ — latin capital letter k with oblique stroke
			['\uA740'] = "K", // Ꝁ — latin capital letter k with stroke
			['\uA744'] = "K", // Ꝅ — latin capital letter k with stroke and diagonal stroke
			['\u1DEC'] = "l", // ᷬ — combining latin small letter l with double middle tilde
			['\uFF4C'] = "l", // ｌ — fullwidth latin small letter l
			['\u019A'] = "l", // ƚ — latin small letter l with bar
			['\u026C'] = "l", // ɬ — latin small letter l with belt
			['\u0234'] = "l", // ȴ — latin small letter l with curl
			['\u2C61'] = "l", // ⱡ — latin small letter l with double bar
			['\uAB38'] = "l", // ꬸ — latin small letter l with double middle tilde
			['\uA749'] = "l", // ꝉ — latin small letter l with high stroke
			['\uAB37'] = "l", // ꬷ — latin small letter l with inverted lazy s
			['\u0140'] = "l", // ŀ — latin small letter l with middle dot
			['\uAB39'] = "l", // ꬹ — latin small letter l with middle ring
			['\u026B'] = "l", // ɫ — latin small letter l with middle tilde
			['\u1D85'] = "l", // ᶅ — latin small letter l with palatal hook
			['\u026D'] = "l", // ɭ — latin small letter l with retroflex hook
			['\uA78E'] = "l", // ꞎ — latin small letter l with retroflex hook and belt
			['\u0142'] = "l", // ł — latin small letter l with stroke
			['\u2097'] = "l", // ₗ — latin subscript small letter l
			['\uFF2C'] = "L", // Ｌ — fullwidth latin capital letter l
			['\u023D'] = "L", // Ƚ — latin capital letter l with bar
			['\uA7AD'] = "L", // Ɬ — latin capital letter l with belt
			['\u2C60'] = "L", // Ⱡ — latin capital letter l with double bar
			['\uA748'] = "L", // Ꝉ — latin capital letter l with high stroke
			['\u013F'] = "L", // Ŀ — latin capital letter l with middle dot
			['\u2C62'] = "L", // Ɫ — latin capital letter l with middle tilde
			['\u0141'] = "L", // Ł — latin capital letter l with stroke
			['\uFF4D'] = "m", // ｍ — fullwidth latin small letter m
			['\uAB3A'] = "m", // ꬺ — latin small letter m with crossed-tail
			['\u0271'] = "m", // ɱ — latin small letter m with hook
			['\u1D6F'] = "m", // ᵯ — latin small letter m with middle tilde
			['\u1D86'] = "m", // ᶆ — latin small letter m with palatal hook
			['\u2098'] = "m", // ₘ — latin subscript small letter m
			['\uFF2D'] = "M", // Ｍ — fullwidth latin capital letter m
			['\u2C6E'] = "M", // Ɱ — latin capital letter m with hook
			['\uFF4E'] = "n", // ｎ — fullwidth latin small letter n
			['\u0149'] = "n", // ŉ — latin small letter n preceded by apostrophe
			['\uAB3B'] = "n", // ꬻ — latin small letter n with crossed-tail
			['\u0235'] = "n", // ȵ — latin small letter n with curl
			['\uA791'] = "n", // ꞑ — latin small letter n with descender
			['\u0272'] = "n", // ɲ — latin small letter n with left hook
			['\u019E'] = "n", // ƞ — latin small letter n with long right leg
			['\u1D70'] = "n", // ᵰ — latin small letter n with middle tilde
			['\uA7A5'] = "n", // ꞥ — latin small letter n with oblique stroke
			['\u1D87'] = "n", // ᶇ — latin small letter n with palatal hook
			['\u0273'] = "n", // ɳ — latin small letter n with retroflex hook
			['\u2099'] = "n", // ₙ — latin subscript small letter n
			['\u207F'] = "n", // ⁿ — superscript latin small letter n
			['\uFF2E'] = "N", // Ｎ — fullwidth latin capital letter n
			['\uA790'] = "N", // Ꞑ — latin capital letter n with descender
			['\u019D'] = "N", // Ɲ — latin capital letter n with left hook
			['\u0220'] = "N", // Ƞ — latin capital letter n with long right leg
			['\uA7A4'] = "N", // Ꞥ — latin capital letter n with oblique stroke
			['\u1DF3'] = "o", // ᷳ — combining latin small letter o with diaeresis
			['\u1DED'] = "o", // ᷭ — combining latin small letter o with light centralization stroke
			['\uFF4F'] = "o", // ｏ — fullwidth latin small letter o
			['\uA74B'] = "o", // ꝋ — latin small letter o with long stroke overlay
			['\uA74D'] = "o", // ꝍ — latin small letter o with loop
			['\u2C7A'] = "o", // ⱺ — latin small letter o with low ring inside
			['\u00F8'] = "o", // ø — latin small letter o with stroke
			['\u2092'] = "o", // ₒ — latin subscript small letter o
			['\uFF2F'] = "O", // Ｏ — fullwidth latin capital letter o
			['\uA74A'] = "O", // Ꝋ — latin capital letter o with long stroke overlay
			['\uA74C'] = "O", // Ꝍ — latin capital letter o with loop
			['\u019F'] = "O", // Ɵ — latin capital letter o with middle tilde
			['\u00D8'] = "O", // Ø — latin capital letter o with stroke
			['\u1DEE'] = "p", // ᷮ — combining latin small letter p
			['\uFF50'] = "p", // ｐ — fullwidth latin small letter p
			['\uA753'] = "p", // ꝓ — latin small letter p with flourish
			['\u01A5'] = "p", // ƥ — latin small letter p with hook
			['\u1D71'] = "p", // ᵱ — latin small letter p with middle tilde
			['\u1D88'] = "p", // ᶈ — latin small letter p with palatal hook
			['\uA755'] = "p", // ꝕ — latin small letter p with squirrel tail
			['\u1D7D'] = "p", // ᵽ — latin small letter p with stroke
			['\uA751'] = "p", // ꝑ — latin small letter p with stroke through descender
			['\u209A'] = "p", // ₚ — latin subscript small letter p
			['\uFF30'] = "P", // Ｐ — fullwidth latin capital letter p
			['\uA752'] = "P", // Ꝓ — latin capital letter p with flourish
			['\u01A4'] = "P", // Ƥ — latin capital letter p with hook
			['\uA754'] = "P", // Ꝕ — latin capital letter p with squirrel tail
			['\u2C63'] = "P", // Ᵽ — latin capital letter p with stroke
			['\uA750'] = "P", // Ꝑ — latin capital letter p with stroke through descender
			['\uFF51'] = "q", // ｑ — fullwidth latin small letter q
			['\uA759'] = "q", // ꝙ — latin small letter q with diagonal stroke
			['\u02A0'] = "q", // ʠ — latin small letter q with hook
			['\u024B'] = "q", // ɋ — latin small letter q with hook tail
			['\uA757'] = "q", // ꝗ — latin small letter q with stroke through descender
			['\uFF31'] = "Q", // Ｑ — fullwidth latin capital letter q
			['\uA758'] = "Q", // Ꝙ — latin capital letter q with diagonal stroke
			['\uA756'] = "Q", // Ꝗ — latin capital letter q with stroke through descender
			['\uFF52'] = "r", // ｒ — fullwidth latin small letter r
			['\uA75B'] = "r", // ꝛ — latin small letter r rotunda
			['\uAB49'] = "r", // ꭉ — latin small letter r with crossed-tail
			['\u027E'] = "r", // ɾ — latin small letter r with fishhook
			['\u1D73'] = "r", // ᵳ — latin small letter r with fishhook and middle tilde
			['\u027C'] = "r", // ɼ — latin small letter r with long leg
			['\u1D72'] = "r", // ᵲ — latin small letter r with middle tilde
			['\uA7A7'] = "r", // ꞧ — latin small letter r with oblique stroke
			['\u1D89'] = "r", // ᶉ — latin small letter r with palatal hook
			['\u024D'] = "r", // ɍ — latin small letter r with stroke
			['\u027D'] = "r", // ɽ — latin small letter r with tail
			['\uAB47'] = "r", // ꭇ — latin small letter r without handle
			['\u1D63'] = "r", // ᵣ — latin subscript small letter r
			['\uFF32'] = "R", // Ｒ — fullwidth latin capital letter r
			['\uA75A'] = "R", // Ꝛ — latin capital letter r rotunda
			['\uA7A6'] = "R", // Ꞧ — latin capital letter r with oblique stroke
			['\u024C'] = "R", // Ɍ — latin capital letter r with stroke
			['\u2C64'] = "R", // Ɽ — latin capital letter r with tail
			['\uFF53'] = "s", // ｓ — fullwidth latin small letter s
			['\u0282'] = "s", // ʂ — latin small letter s with hook
			['\u1D74'] = "s", // ᵴ — latin small letter s with middle tilde
			['\uA7A9'] = "s", // ꞩ — latin small letter s with oblique stroke
			['\u1D8A'] = "s", // ᶊ — latin small letter s with palatal hook
			['\u023F'] = "s", // ȿ — latin small letter s with swash tail
			['\u209B'] = "s", // ₛ — latin subscript small letter s
			['\uFF33'] = "S", // Ｓ — fullwidth latin capital letter s
			['\uA7A8'] = "S", // Ꞩ — latin capital letter s with oblique stroke
			['\u2C7E'] = "S", // Ȿ — latin capital letter s with swash tail
			['\uFF54'] = "t", // ｔ — fullwidth latin small letter t
			['\u0236'] = "t", // ȶ — latin small letter t with curl
			['\u2C66'] = "t", // ⱦ — latin small letter t with diagonal stroke
			['\u01AD'] = "t", // ƭ — latin small letter t with hook
			['\u1D75'] = "t", // ᵵ — latin small letter t with middle tilde
			['\u01AB'] = "t", // ƫ — latin small letter t with palatal hook
			['\u0288'] = "t", // ʈ — latin small letter t with retroflex hook
			['\u0167'] = "t", // ŧ — latin small letter t with stroke
			['\u209C'] = "t", // ₜ — latin subscript small letter t
			['\uFF34'] = "T", // Ｔ — fullwidth latin capital letter t
			['\u023E'] = "T", // Ⱦ — latin capital letter t with diagonal stroke
			['\u01AC'] = "T", // Ƭ — latin capital letter t with hook
			['\u01AE'] = "T", // Ʈ — latin capital letter t with retroflex hook
			['\u0166'] = "T", // Ŧ — latin capital letter t with stroke
			['\u1DF4'] = "u", // ᷴ — combining latin small letter u with diaeresis
			['\u1DF0'] = "u", // ᷰ — combining latin small letter u with light centralization stroke
			['\uFF55'] = "u", // ｕ — fullwidth latin small letter u
			['\u1D7E'] = "u", // ᵾ — latin small capital letter u with stroke
			['\u0289'] = "u", // ʉ — latin small letter u bar
			['\uAB4F'] = "u", // ꭏ — latin small letter u bar with short right leg
			['\uAB52'] = "u", // ꭒ — latin small letter u with left hook
			['\u1D99'] = "u", // ᶙ — latin small letter u with retroflex hook
			['\uAB4E'] = "u", // ꭎ — latin small letter u with short right leg
			['\u1D64'] = "u", // ᵤ — latin subscript small letter u
			['\uFF35'] = "U", // Ｕ — fullwidth latin capital letter u
			['\u0244'] = "U", // Ʉ — latin capital letter u bar
			['\uFF56'] = "v", // ｖ — fullwidth latin small letter v
			['\u2C74'] = "v", // ⱴ — latin small letter v with curl
			['\uA75F'] = "v", // ꝟ — latin small letter v with diagonal stroke
			['\u028B'] = "v", // ʋ — latin small letter v with hook
			['\u1D8C'] = "v", // ᶌ — latin small letter v with palatal hook
			['\u2C71'] = "v", // ⱱ — latin small letter v with right hook
			['\u1D65'] = "v", // ᵥ — latin subscript small letter v
			['\uFF36'] = "V", // Ｖ — fullwidth latin capital letter v
			['\uA75E'] = "V", // Ꝟ — latin capital letter v with diagonal stroke
			['\u01B2'] = "V", // Ʋ — latin capital letter v with hook
			['\u1DF1'] = "w", // ᷱ — combining latin small letter w
			['\uFF57'] = "w", // ｗ — fullwidth latin small letter w
			['\u2C73'] = "w", // ⱳ — latin small letter w with hook
			['\uFF37'] = "W", // Ｗ — fullwidth latin capital letter w
			['\u2C72'] = "W", // Ⱳ — latin capital letter w with hook
			['\uFF58'] = "x", // ｘ — fullwidth latin small letter x
			['\uAB57'] = "x", // ꭗ — latin small letter x with long left leg
			['\uAB58'] = "x", // ꭘ — latin small letter x with long left leg and low right ring
			['\uAB59'] = "x", // ꭙ — latin small letter x with long left leg with serif
			['\uAB56'] = "x", // ꭖ — latin small letter x with low right ring
			['\u1D8D'] = "x", // ᶍ — latin small letter x with palatal hook
			['\u2093'] = "x", // ₓ — latin subscript small letter x
			['\uFF38'] = "X", // Ｘ — fullwidth latin capital letter x
			['\uFF59'] = "y", // ｙ — fullwidth latin small letter y
			['\u01B4'] = "y", // ƴ — latin small letter y with hook
			['\u1EFF'] = "y", // ỿ — latin small letter y with loop
			['\uAB5A'] = "y", // ꭚ — latin small letter y with short right leg
			['\u024F'] = "y", // ɏ — latin small letter y with stroke
			['\uFF39'] = "Y", // Ｙ — fullwidth latin capital letter y
			['\u01B3'] = "Y", // Ƴ — latin capital letter y with hook
			['\u1EFE'] = "Y", // Ỿ — latin capital letter y with loop
			['\u024E'] = "Y", // Ɏ — latin capital letter y with stroke
			['\uFF5A'] = "z", // ｚ — fullwidth latin small letter z
			['\u0291'] = "z", // ʑ — latin small letter z with curl
			['\u2C6C'] = "z", // ⱬ — latin small letter z with descender
			['\u0225'] = "z", // ȥ — latin small letter z with hook
			['\u1D76'] = "z", // ᵶ — latin small letter z with middle tilde
			['\u1D8E'] = "z", // ᶎ — latin small letter z with palatal hook
			['\u0290'] = "z", // ʐ — latin small letter z with retroflex hook
			['\u01B6'] = "z", // ƶ — latin small letter z with stroke
			['\u0240'] = "z", // ɀ — latin small letter z with swash tail
			['\uFF3A'] = "Z", // Ｚ — fullwidth latin capital letter z
			['\u2C6B'] = "Z", // Ⱬ — latin capital letter z with descender
			['\u0224'] = "Z", // Ȥ — latin capital letter z with hook
			['\u01B5'] = "Z", // Ƶ — latin capital letter z with stroke
			['\u2C7F'] = "Z", // Ɀ — latin capital letter z with swash tail
			['\u01F2'] = "Dz", // ǲ — latin capital letter d with small letter z
			['\u01C5'] = "Dz", // ǅ — latin capital letter d with small letter z with caron
			['\u01C8'] = "Lj", // ǈ — latin capital letter l with small letter j
			['\u01CB'] = "Nj", // ǋ — latin capital letter n with small letter j
			['\u24D0'] = "(a)", // ⓐ — circled latin small letter a
			['\u249C'] = "(a)", // ⒜ — parenthesized latin small letter a
			['\u24B6'] = "(A)", // Ⓐ — circled latin capital letter a
			['\u24D1'] = "(b)", // ⓑ — circled latin small letter b
			['\u249D'] = "(b)", // ⒝ — parenthesized latin small letter b
			['\u24B7'] = "(B)", // Ⓑ — circled latin capital letter b
			['\u24D2'] = "(c)", // ⓒ — circled latin small letter c
			['\u249E'] = "(c)", // ⒞ — parenthesized latin small letter c
			['\u24B8'] = "(C)", // Ⓒ — circled latin capital letter c
			['\u24D3'] = "(d)", // ⓓ — circled latin small letter d
			['\u249F'] = "(d)", // ⒟ — parenthesized latin small letter d
			['\u24B9'] = "(D)", // Ⓓ — circled latin capital letter d
			['\u24D4'] = "(e)", // ⓔ — circled latin small letter e
			['\u24A0'] = "(e)", // ⒠ — parenthesized latin small letter e
			['\u24BA'] = "(E)", // Ⓔ — circled latin capital letter e
			['\u24D5'] = "(f)", // ⓕ — circled latin small letter f
			['\u24A1'] = "(f)", // ⒡ — parenthesized latin small letter f
			['\u24BB'] = "(F)", // Ⓕ — circled latin capital letter f
			['\u24D6'] = "(g)", // ⓖ — circled latin small letter g
			['\u24A2'] = "(g)", // ⒢ — parenthesized latin small letter g
			['\u24BC'] = "(G)", // Ⓖ — circled latin capital letter g
			['\u24D7'] = "(h)", // ⓗ — circled latin small letter h
			['\u24A3'] = "(h)", // ⒣ — parenthesized latin small letter h
			['\u24BD'] = "(H)", // Ⓗ — circled latin capital letter h
			['\u24D8'] = "(i)", // ⓘ — circled latin small letter i
			['\u24A4'] = "(i)", // ⒤ — parenthesized latin small letter i
			['\u24BE'] = "(I)", // Ⓘ — circled latin capital letter i
			['\u24D9'] = "(j)", // ⓙ — circled latin small letter j
			['\u24A5'] = "(j)", // ⒥ — parenthesized latin small letter j
			['\u24BF'] = "(J)", // Ⓙ — circled latin capital letter j
			['\u24DA'] = "(k)", // ⓚ — circled latin small letter k
			['\u24A6'] = "(k)", // ⒦ — parenthesized latin small letter k
			['\u24C0'] = "(K)", // Ⓚ — circled latin capital letter k
			['\u24DB'] = "(l)", // ⓛ — circled latin small letter l
			['\u24A7'] = "(l)", // ⒧ — parenthesized latin small letter l
			['\u24C1'] = "(L)", // Ⓛ — circled latin capital letter l
			['\u24DC'] = "(m)", // ⓜ — circled latin small letter m
			['\u24A8'] = "(m)", // ⒨ — parenthesized latin small letter m
			['\u24C2'] = "(M)", // Ⓜ — circled latin capital letter m
			['\u24DD'] = "(n)", // ⓝ — circled latin small letter n
			['\u24A9'] = "(n)", // ⒩ — parenthesized latin small letter n
			['\u24C3'] = "(N)", // Ⓝ — circled latin capital letter n
			['\u24DE'] = "(o)", // ⓞ — circled latin small letter o
			['\u24AA'] = "(o)", // ⒪ — parenthesized latin small letter o
			['\u24C4'] = "(O)", // Ⓞ — circled latin capital letter o
			['\u24DF'] = "(p)", // ⓟ — circled latin small letter p
			['\u24AB'] = "(p)", // ⒫ — parenthesized latin small letter p
			['\u24C5'] = "(P)", // Ⓟ — circled latin capital letter p
			['\u24E0'] = "(q)", // ⓠ — circled latin small letter q
			['\u24AC'] = "(q)", // ⒬ — parenthesized latin small letter q
			['\u24C6'] = "(Q)", // Ⓠ — circled latin capital letter q
			['\u24E1'] = "(r)", // ⓡ — circled latin small letter r
			['\u24AD'] = "(r)", // ⒭ — parenthesized latin small letter r
			['\u24C7'] = "(R)", // Ⓡ — circled latin capital letter r
			['\u24E2'] = "(s)", // ⓢ — circled latin small letter s
			['\u24AE'] = "(s)", // ⒮ — parenthesized latin small letter s
			['\u24C8'] = "(S)", // Ⓢ — circled latin capital letter s
			['\u24E3'] = "(t)", // ⓣ — circled latin small letter t
			['\u24AF'] = "(t)", // ⒯ — parenthesized latin small letter t
			['\u24C9'] = "(T)", // Ⓣ — circled latin capital letter t
			['\u24E4'] = "(u)", // ⓤ — circled latin small letter u
			['\u24B0'] = "(u)", // ⒰ — parenthesized latin small letter u
			['\u24CA'] = "(U)", // Ⓤ — circled latin capital letter u
			['\u24E5'] = "(v)", // ⓥ — circled latin small letter v
			['\u24B1'] = "(v)", // ⒱ — parenthesized latin small letter v
			['\u24CB'] = "(V)", // Ⓥ — circled latin capital letter v
			['\u24E6'] = "(w)", // ⓦ — circled latin small letter w
			['\u24B2'] = "(w)", // ⒲ — parenthesized latin small letter w
			['\u24CC'] = "(W)", // Ⓦ — circled latin capital letter w
			['\u24E7'] = "(x)", // ⓧ — circled latin small letter x
			['\u24B3'] = "(x)", // ⒳ — parenthesized latin small letter x
			['\u24CD'] = "(X)", // Ⓧ — circled latin capital letter x
			['\u24E8'] = "(y)", // ⓨ — circled latin small letter y
			['\u24B4'] = "(y)", // ⒴ — parenthesized latin small letter y
			['\u24CE'] = "(Y)", // Ⓨ — circled latin capital letter y
			['\u24E9'] = "(z)", // ⓩ — circled latin small letter z
			['\u24B5'] = "(z)", // ⒵ — parenthesized latin small letter z
			['\u24CF'] = "(Z)", // Ⓩ — circled latin capital letter z
		};

		private static readonly Lazy<Regex> UnescapeRegex = new Lazy<Regex>(() => new Regex(
			@"\\(['""\\0abfnrtv]|x[a-fA-F0-9]{1,4}|u[a-fA-F0-9]{4}|U[a-fA-F0-9]{8}|)",
			RegexOptions.Compiled | RegexOptions.ExplicitCapture));

		/// <summary>
		///     Removes non-spacing marks from all characters, such as the accent in 'resumé'.
		/// </summary>
		/// <param name="s">A string to remove non-spacing marks from.</param>
		/// <returns>A string with non-spacing marks removed.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="s" /> is <c>null</c>.</exception>
		/// <seealso cref="UnicodeCategory.NonSpacingMark" />
		[Pure]
		[Obsolete("This functionality is an incomplete subset of that provided by ToAscii.")]
		public static string RemoveDiacritics(this string s)
		{
			Contract.Requires<ArgumentNullException>(s != null);
			Contract.Ensures(Contract.Result<string>() != null);

			var d = s.Normalize(NormalizationForm.FormD);
			var sb = new StringBuilder(s.Length);
			// ReSharper disable LoopCanBePartlyConvertedToQuery
			foreach (var ch in d)
			{
				var uc = CharUnicodeInfo.GetUnicodeCategory(ch);
				if (uc != UnicodeCategory.NonSpacingMark)
					sb.Append(ch);
			}
			// ReSharper restore LoopCanBePartlyConvertedToQuery
			return (sb.ToString().Normalize(NormalizationForm.FormC));
		}

		/// <summary>
		///     Removes non-spacing marks from all characters, and replaces certain non-ASCII characters with similar ASCII strings.
		/// </summary>
		/// <param name="s">A string whose letters to convert to ASCII.</param>
		/// <returns>A string with certain non-ASCII characters replaced with similar ASCII strings.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="s" /> is <c>null</c>.</exception>
		[Pure]
		public static string ToAscii(this string s)
		{
			Contract.Requires<ArgumentNullException>(s != null);
			Contract.Ensures(Contract.Result<string>() != null);

			var formD = s.Normalize(NormalizationForm.FormD);
			var result = new StringBuilder(s.Length);
			foreach (var ch in formD)
			{
				var category = CharUnicodeInfo.GetUnicodeCategory(ch);
				if (category == UnicodeCategory.NonSpacingMark) continue;

				string ascii;
				if (UnicodeToAscii.TryGetValue(ch, out ascii))
				{
					result.Append(ascii);
					continue;
				}

				result.Append(ch);
			}
			return (result.ToString().Normalize(NormalizationForm.FormC));
		}

		/// <summary>
		///     Converts the string to a url-safe slug containing only alphanumerics and the dash character. Whitespace and
		///     punctuation are either ignored or replaced with dashes, while diacritics are removed and certain common Latin
		///     ligatures are replaced. Optionally converts open-close punctuation to parenthesis.
		/// </summary>
		/// <param name="s">A string to convert to a slug.</param>
		/// <param name="lowercase">Whether or not to convert all alpha characters to lowercase.</param>
		/// <param name="parenthetical">Whether or not to convert open and close punctuations to matched parentheses.</param>
		/// <param name="strict">Whether or not to fail when encountering unhandled characters.</param>
		/// <returns>A url-safe slug containing only alphanumerics, the dash character, and optional parentheses.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="s" /> is <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		///     <paramref name="s" /> contains unhandled characters and <paramref name="strict" /> is <c>true</c>.
		/// </exception>
		/// <remarks>
		///     This method takes a very visually-biased and linguistically incorrect approach to certain foreign characters, such
		///     as mu (µ) to u, thorn (þ) to p, etc. Any more-delicate handling of these characters should be done beforehand.
		/// </remarks>
		/// <seealso cref="UnicodeCategory" />
		[Pure]
		public static string ToSlug(this string s, bool lowercase = false,
			bool parenthetical = false, bool strict = false)
		{
			// TODO: Refactor to use ToAscii method

			Contract.Requires<ArgumentNullException>(s != null);
			Contract.Ensures(Contract.Result<string>() != null);

			if (s.Length == 0) return string.Empty;

			// Normalize to FormD in order to remove diacritics.
			var d = s.Normalize(NormalizationForm.FormD);
			var sb = new StringBuilder(s.Length);

			var wordbreak = false;
			var skipbreak = false;
			var parenCount = 0;
			foreach (var ch in d)
			{
				string append;
				var skipnext = false;
				if ((ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z') || (ch >= '0' && ch <= '9') || ch == '_')
				{
					// Character is an alphanumeric
					if (lowercase && (ch >= 'A' && ch <= 'Z'))
					{
						// Append the lowercase form of this character
						append = char.ToLower(ch).ToString();
					}
					else
					{
						// Append this character as-is
						append = ch.ToString();
					}
				}
				else if (char.IsWhiteSpace(ch) || char.IsSeparator(ch) ||
				         ch == '-' || ch == '–' || ch == '—' || ch == '/' || ch == '\\')
				{
					// Break on this character.
					// Catches control-characters and other punctuations that UnicodeCategory does not.
					// It also catches certain common characters to avoid dictionary and category lookups.
					wordbreak = true;
					continue;
				}
				else if (UnicodeToAscii.TryGetValue(ch, out append))
				{
					// We have an (approximate) slug equivalent for this character.
					if (lowercase)
					{
						// We don't know if the value is lowercase or not, so we'll be safe.
						append = append.ToLowerInvariant();
					}
				}
				else
				{
					var uc = CharUnicodeInfo.GetUnicodeCategory(ch);
					switch (uc)
					{
						case UnicodeCategory.SpaceSeparator:
						case UnicodeCategory.DashPunctuation:
						case UnicodeCategory.ConnectorPunctuation:
							// Character causes word-breaks when between two alphanumeric characters.
							wordbreak = true;
							continue;
						case UnicodeCategory.OpenPunctuation:
							if (parenthetical)
							{
								// Append parenthese and skip next wordbreak
								append = "(";
								parenCount++;
								skipnext = true;
								break;
							}
							wordbreak = true;
							goto default;
						case UnicodeCategory.ClosePunctuation:
							if (parenthetical && parenCount > 0)
							{
								// Skip current wordbreak, append parenthese,
								append = ")";
								parenCount--;
								skipbreak = true;
								break;
							}
							wordbreak = true;
							goto default;
						case UnicodeCategory.MathSymbol:
							wordbreak = true;
							goto default;
						default:
							if (!strict) continue;
							var character = uc != UnicodeCategory.Control ? ch.ToString() : string.Empty;
							throw new ArgumentOutOfRangeException(nameof(s), ch,
								$"Unhandled character in UnicodeCategory.{uc}: '{character}' (0x{ch.ToHex()}).");
					}
				}
				if (wordbreak && !skipbreak && sb.Length != 0)
				{
					// We've added at least one alphanumeric character, we're
					// about to add another, and there was a break inbetween.
					sb.Append('-');
				}
				// Append character and reset whether or not we're in a break.
				sb.Append(append);
				wordbreak = false;
				skipbreak = skipnext;
			}
			if (parenthetical)
			{
				for (; parenCount > 0; parenCount--)
					sb.Append(")");
			}
			// Return slug re-normalized to FormC.
			return (sb.ToString().Normalize(NormalizationForm.FormC));
		}

		/// <summary>
		///     Converts the string to an unescaped string, following the rules for C# string literal escaping.
		/// </summary>
		/// <param name="s">A string to unescape.</param>
		/// <returns>An unescaped string.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="s" /> is <c>null</c>.</exception>
		/// <exception cref="FormatException"><paramref name="s" /> contains a backslash followed by invalid characters.</exception>
		/// <seealso cref="UnescapeVerbatim" />
		/// <remarks>
		///     The valid escape sequences can be found here under "Escaping in character and string literals":
		///     http://www.codeproject.com/Articles/371232/Escaping-in-Csharp-characters-strings-string-forma
		/// </remarks>
		[Pure]
		public static string Unescape(this string s)
		{
			Contract.Requires<ArgumentNullException>(s != null);
			Contract.Ensures(Contract.Result<string>() != null);

			if (s.Length == 0) return string.Empty;
			var match = UnescapeRegex.Value.Match(s);
			if (match.Success == false) return s;

			var position = 0;
			var sb = new StringBuilder(s.Length);
			do
			{
				if (match.Value.Length == 1)
				{
					throw new FormatException($"Invalid escape sequence found at character {match.Index}.");
				}
				// Append non-escaped characters between last match and this match
				if (match.Index > position) sb.Append(s, position, match.Index - position);
				// Append escaped character
				switch (match.Value[1])
				{
					case '\'':
						sb.Append('\'');
						break;
					case '"':
						sb.Append('\"');
						break;
					case '\\':
						sb.Append('\\');
						break;
					case '0':
						sb.Append('\0');
						break;
					case 'a':
						sb.Append('\a');
						break;
					case 'b':
						sb.Append('\b');
						break;
					case 'f':
						sb.Append('\f');
						break;
					case 'n':
						sb.Append('\n');
						break;
					case 'r':
						sb.Append('\r');
						break;
					case 't':
						sb.Append('\t');
						break;
					case 'v':
						sb.Append('\v');
						break;
					case 'u':
					case 'x':
						// Append single escaped Unicode character
						var c = (char)int.Parse(match.Value.Substring(2), NumberStyles.HexNumber);
						sb.Append(c);
						break;
					case 'U':
						// Append surrogate pair of Unicode characters
						var p = char.ConvertFromUtf32(int.Parse(match.Value.Substring(2), NumberStyles.HexNumber));
						sb.Append(p);
						break;
				}
				// Increment position to end oft his match
				position = match.Index + match.Length;
				// Move to the next match, if there is one
			} while ((match = match.NextMatch()).Success);
			// Append non-escaped characters between last match and end
			if (position < s.Length) sb.Append(s, position, s.Length - position);
			return sb.ToString();
		}

		/// <summary>
		///     Converts the string to an unescaped string, following the rules for C# verbatim string escaping.
		/// </summary>
		/// <param name="s">A string to unescape.</param>
		/// <returns>An unescaped string.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="s" /> is <c>null</c>.</exception>
		/// <exception cref="FormatException"><paramref name="s" /> contains a quotation mark not immediately followed by another.</exception>
		/// <seealso cref="Unescape" />
		[Pure]
		public static string UnescapeVerbatim(this string s)
		{
			Contract.Requires<ArgumentNullException>(s != null);
			Contract.Ensures(Contract.Result<string>() != null);

			if (s.Length == 0) return string.Empty;
			var match = s.IndexOf('"');
			if (match == -1) return s;

			var sb = new StringBuilder(s.Length);
			var position = 0;
			do
			{
				if (match == s.Length - 1 || s[match + 1] != '"')
				{
					throw new FormatException($"Invalid escape sequence found at character {match}.");
				}
				// Append non-escaped characters between last match and this match
				if (match > position) sb.Append(s, position, match - position);
				sb.Append('"');
				// Increment position to end oft his match
				position = match + 2;
				// Move to the next match, if there is one
			} while ((match = s.IndexOf('"', position)) != -1);
			// Append non-escaped characters between last match and end
			if (position < s.Length) sb.Append(s, position, s.Length - position);
			return sb.ToString();
		}
	}
}
