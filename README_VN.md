**Đây project tutorial để giúp bạn hiểu cách sử dụng công cụ [Excel2Unity](https://github.com/nbhung100914/excel-to-unity).**

# Bắt đầu

Trong Repository này bao gồm hai phần chính:

- Phần cơ bản: phần này giúp bạn nắm bắt rõ hơn về việc thiết kế data với Excel2Unity.
- Phần nâng cao: trong phần này, chúng ta sẽ tìm hiểu cách một Game RPG mid-core thực tế sử dụng công cụ này.

Tuy nhiên, trong bài viết này, tôi chỉ đề cập đến phần cơ bản, để tránh làm cho bài viết trở nên quá dài nếu đề cập đến cả hai phần.

## 1. Cấu trúc của file Excel

Đầu tiên, hãy mở file excel tại địa chỉ `/Assets/Basic/Data/Example.xlsx`. Đây là file Excel mẫu. Trong file này chứa data mẫu giúp bạn hiểu cách thiết kế các loại data như IDs, Constants, và Data Table.

![excel-to-unity-basic-excel-file](https://github.com/nbhung100914/excel-to-unity-example/assets/9100041/87454ce5-e7a3-489d-8ffe-2cecb647622c)

### Sheet Constants:

```
| Name | Type | Value | Comment |
| ---- | ---- | ----- | ------- |
```

- Tên sheet cần có `Constants` làm tiền tố hoặc hậu tố.
- Có bốn cột: Name, Type, Value, và Comment.
- Name: Đây là tên của hằng số, phải viết liền, không chứa ký tự đặc biệt và nên viết in hoa.
- Type: Đây là kiểu dữ liệu của hằng số. Bạn có thể sử dụng các kiểu dữ liệu sau: `int`, `float`, `bool`, `string`, `int-array`, `float-array`, `vector2`, và `vector3`.
- Value: Giá trị tương ứng với kiểu dữ liệu. Đối với kiểu dữ liệu array, các phần tử phải được phân tách bằng dấu `:` hoặc `|` hoặc `xuống dòng`.

### Sheet IDs:

```
| Group | Key | Comment |
| ----- | --- | ------- |
```

- Tên sheet cần có `IDs` ở tiền tố hoặc hậu tố.
- Trong Sheet này, chỉ sử dụng kiểu dữ liệu Integer.
- Mỗi nhóm được sắp xếp trong 3 cột liên tiếp.
- Dòng đầu tiên chứa tên nhóm để thuận tiện cho việc tra cứu.
- Cột đầu tiên chứa Key Name, còn cột kế tiếp chứa Key Value.
- Key Value phải là số nguyên.
- Mặc định, tất cả id của một cột sẽ được xuất ra dưới dạng Integer Constants, nhưng bạn cũng có thể xuất chúng dưới dạng enum bằng cách thêm hậu tố [enum] vào tên nhóm.
- Bạn có thể chọn chỉ xuất enum và bỏ qua Integer Constant bằng cách chọn `Only enum as IDs` trong phần Settings.

### Sheet Localization:

```
| idString | relativeId | english | spanish | japan | .... |
| -------- | ---------- | ------- | ------- | ----- | ---- |
```

- Tên sheet cần có `Localization` làm tiền tố hoặc hậu tố.
- Sheet này có cấu trúc gồm 2 cột key, một là key chính `idString` và một là key bổ sung `relativeId`.
- Các cột tiếp theo sẽ chứa các nội dung đã được nội địa hóa.
- Key của một dòng là sự kết hợp của `idString` và `relativeId`.

```
VD idString:"hero_name" và relativeId:1 thì key sẽ là hero_name_1
```

- `relativeId` có thể tham chiếu tới id của sheet IDs.

### Sheet Data Table

- Tên của sheet data table không được chứa chuỗi `IDs`, `Constants` và `Localization`
- Sheet này có thể sử dụng các kiểu dữ liệu sau: `number`, `string`, `boolean`, `list/array`, `JSON object` và `attribute object`.
- Dòng đầu tiên dùng để đặt tên cho các trường dữ liệu, cột không có tên sẽ bị bỏ qua khi xuất Json data.
- Thêm hậu tố `[]` vào tên cột, để định nghĩa kiểu dữ liệu `list/array`.
- Thêm hậu tố `{}` vào tên cột, để định nghĩa kiểu dữ liệu `JSON object`.
- Các ô có giá trị rỗng, bằng 0 hoặc bằng FALSE sẽ bị bỏ qua khi xuất Json Data.
- Các cột chỉ có tên mà không có giá trị, giá trị rỗng, bằng 0 hoặc bằng FALSE sẽ bị bỏ qua khi xuất JSON Data. Điều này giúp tránh dư thừa dữ liệu, tối ưu kích thước của JSON Data.
- Để giữ lại các cột tránh bị bỏ qua, thì cần thêm tên của cột đó vào ô `Unminimized Fields`
- Thêm hậu tố `[x]` vào tên cột, để loại bỏ cột đó, không xuất ra Json data.
- Để định nghĩa kiểu attribute object. thì cần tuân theo các quy tắc sau:

  - Cột attribute phải được đặt ở cuối bảng dữ liệu.
  - Attribute id là constant integer, nên được định nghĩa trong sheet IDs.
  - Một attribute có cấu trúc như sau:

    1. **`attribute`**: Tên cột tuân theo mẫu _`attribute + (index)`_, trong đó index có thể là một số bất kỳ, nhưng nên bắt đầu từ 0 và tăng dần. Giá trị của cột này là id của attribute, có kiểu Integer, giá trị này nên được thiết lập tại sheet IDs.
    2. **`value`**: Tên cột tuân theo mẫu _`value + (index)`_ hoặc _`value + (index) + []`_, giá trị của cột có thể là number hoặc number array.
    3. **`increase`**: Tên cột tuân theo mẫu _`increase + (index)`_ hoặc _`increase + (index) + []`_. Đây là giá trị bổ sung, có thể có hoặc không, thường dùng cho tình huống level-up, quy định chỉ số tăng thêm khi nhân vật hoặc item level-up.
    4. **`unlock`**: Tên cột tuân theo mẫu _`unlock + (index)`_ hoặc _`unlock + (index) + []`_. Đây là giá trị bổ sung, có thể có hoặc không, thường dùng cho tình huống attribute cần điều kiện để mở khóa, ví dụ như level tối thiểu hoặc rank tối thiểu.
    5. **`max`**: Tên cột tuân theo mẫu _`max + (index)`_ hoặc _`max + (index) + []`_. Đây là giá trị bổ sung, có thể có hoặc không, thường dùng cho tình huống attribute có giá trị tối đa.

    ```
    Example 1: attribute0, value0, increase0, value0, max0.
    Example 2: attribute1, value1[], increase1[], value1[], max1[].
    ```

## 2. Exporting

### Unity

Tạo 3 thư mục để lưu các file sẽ được xuất ra

- `Assets\Basic\Scripts\Generated` để lưu trữ các script IDs và Constants.
- `Assets\Basic\Data` để lưu trữ dữ liệu Json data đã xuất.
- `Assets\Basic\Resources\Data` để lưu trữ dữ liệu Localization data.

### Excel2Unity

Nhập đường dẫn tới các thư mục đã được tạo ở trên, và các thiết lập cần thiết khác.

![excel-to-unity-basic-settings](https://github.com/nbhung100914/excel-to-unity-example/assets/9100041/ed6874aa-b240-40e6-94fc-8494271344d3)

- Json Data Output: `[your project path]\Assets\Basic\Data`.
- Constant Ouput: `[Your project path]\Assets\Basic\Scripts\Generated`, IDs, Constants, Localization API và LocalizationText Component sẽ được lưu tại đây.
- Localization Ouput: `[Your project path]\Assets\Basic\Resources\Data`, Localization data cần phải lưu tại Resources folder để có thể load/unload file ngôn ngữ.
- Namespace: `Excel2Unity.Basic`.
- Languages maps: `korean, japanese, chinese`, chúng ta sẽ tạo bảng ký tự riêng cho 3 ngôn ngữ này

![excel-to-unity-basic-exporting](https://github.com/nbhung100914/excel-to-unity-example/assets/9100041/1adc69e7-06fc-433a-b59c-fc2049a53163)

- Điền đường dẫn tới file excel hoặc chọn file bằng nút `Select File`
- Cuối cùng, ấn `Export Json`, `Export IDs`, `Export Constants` và `Export Localization` để xuất data và các script

Các files được xuất ra sẽ như sau

![excel-to-unity-basic-exported-scripts](https://github.com/nbhung100914/excel-to-unity-example/assets/9100041/365f5526-e0b2-410b-912b-5a7c09710edc)
![excel-to-unity-basic-exported-data](https://github.com/nbhung100914/excel-to-unity-example/assets/9100041/ea6c490d-fa24-4857-b48b-fffc4d85ddcd)
![excel-to-unity-basic-exported-localization](https://github.com/nbhung100914/excel-to-unity-example/assets/9100041/669aa26d-de9c-4218-87da-964d302df6a1)

## 3. Scripting

### Tạo một ScriptableObject làm Static Database

- Tạo các Serializable Object tương tứng với các trường dữ liệu trong các Data Table

```cs
[Serializable]
public class DataBasic1
{
    public int numberExample1;
    public int numberExample2;
    public int numberExample3;
    public bool boolExample;
    public string stringExample;
}
```

```cs
[Serializable]
public class DataBasic2
{
    [Serializable]
    public class Example
    {
        public int id;
        public string name;
    }

    public string[] array1;
    public int[] array2;
    public int[] array3;
    public bool[] array4;
    public int[] array5;
    public string[] array6;
    public Example json1;
}
```

```cs
//NOTE: Để có thể sử dụng được tính năng Attributes, đối tượng cần kế thừa AttributesCollection
[Serializable]
public class DataAdvanced : AttributesCollection<AttributeParse>
{
    public int id;
    public string name;
}
```

- Tạo một ScriptableObject chứa các Serializable Objects trên

```cs
[CreateAssetMenu(fileName = "DataCollectionBasic", menuName = "Excel2Unity/DataCollectionBasic")]
public class DataCollectionBasic : ScriptableObject
{
    public List<DataBasic1> dataBasic1;
    public List<DataBasic2> dataBasic2;
    public List<DataAdvanced> dataAdvanced;
}
```

- Load Json Data vào các Serializable Objects

```cs
// NOTE: hàm này sử dụng thư viện UnityEditor, nên nó phải nằm trong thư mục Editor hoặc phải nằm trong #if UNITY_EDITOR
// Nếu bạn không muốn sử dụng Editor code, bạn có thể chọn lưu trữ các file Json Data trong thư mục Resources hoặc Asset Bundles rồi load bằng phương thức tương ứng
private void LoadData()
{
    var txt =  AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Basic/Data/ExampleDataBasic1.txt");
    m_target.dataBasic1 = JsonHelper.ToList<DataBasic1>(txt.text);
    txt =  AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Basic/Data/ExampleDataBasic2.txt");
    m_target.dataBasic2 = JsonHelper.ToList<DataBasic2>(txt.text);
    txt = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Basic/Data/ExampleDataAdvanced.txt");
    m_target. dataAdvanced = JsonHelper.ToList<DataAdvanced>(txt.text);
}
```

- Cuối cùng, chúng ta sẽ có một Static Database. Mỗi khi có thay đổi, bạn chỉ cần chỉnh sửa trên excel và xuất data mới. Sau đó, trong Unity, bạn chỉ cần Reload Static Database.

![excel-to-unity-basic-scriptable-object](https://github.com/nbhung100914/excel-to-unity-example/assets/9100041/0b4bd9fb-5a24-4ec6-ba7d-05b7caba1f22)
![excel-to-unity-basic-load-data](https://github.com/nbhung100914/excel-to-unity-example/assets/9100041/3452494e-0a13-4953-af79-900c83ec3809)

### Localization

Thay đổi ngôn ngữ.

```cs
// Chọn ngôn ngữ tiếng Tây Ban Nha
Localization.currentLanguage = "spanish";
// Chọn ngôn ngữ tiếng Nhật
Localization.currentLanguage = "japanese";
// Chọn ngôn ngữ tiếng Hàn
Localization.currentLanguage = "korean";
```

Đăng ký một event handler cho sự kiện thay đổi ngôn ngữ.

```cs
Localization.onLanguageChanged += OnLanguageChanged;
```

Truy xuất nội dung đã được nội địa hóa bằng Key, tuy nhiên với cách này Text sẽ được ko tự động cập nhật hiển thị khi ngôn ngữ thay đổi.

```cs
// Truy xuất text đã được nội địa theo integer key
m_txtExample1.text = Localization.Get(Localization.DAY_X, 1).ToString();
// Truy xuất text đã được nội địa theo string key
m_txtExample1.text = Localization.Get("DAY_X", 1).ToString();
```

Bạn có thể liên kết gameObject với một Key để Text tự động cập nhật khi ngôn ngữ thay đổi, gameObject phải chứa Component Text hoặc TextMeshProUGUI.

```cs
// Khai báo một Dynamic Text liên kết với một integer key
Localization.RegisterDynamicText(m_goExample2, Localization.hero_name_5);
// Hoặc liên kết gameObject với một string key
Localization.RegisterDynamicText(m_txtExample2, "hero_name_" + IDs.HERO_5);
// Hủy liên kết gameObject
Localization.UnregisterDynamicText(m_goExample2);
```

Sử dụng Localization Component.

![excel-to-unity-basic-localization-component](https://github.com/nbhung100914/excel-to-unity-example/assets/9100041/28e9453c-2d25-44b3-ae55-ef08adee8063)

### Chia nhỏ Localization

Trong trường hợp bạn chọn Separate Localization trong bảng Settings. Localization data và Localization scripts xuất ra sẽ trông như sau.

![excel-to-unity-basic-exported-multi-localization](https://github.com/nbhung100914/excel-to-unity-example/assets/9100041/000b0c23-c1b8-4596-aa15-bbbc3e009e68)

Localization Code sẽ thay đổi như sau.

```cs
LocalizationsManager.currentLanguage = "spanish";
```

```cs
private IEnumerator Start()
{
    yield return LocalizationsManager.InitAsync(null);
}
```

```cs
LocalizationsManager.onLanguageChanged += OnLanguageChanged;
```

```cs
// Truy xuất text đã được nội địa theo integer key
m_txtExample1.text = ExampleLocalization.Get(ExampleLocalization.DAY_X, 1).ToString();
// Truy xuất text đã được nội địa theo string key
m_txtExample1.text = ExampleLocalization.Get("DAY_X", 1).ToString();
```

```cs
// Khai báo một Dynamic Text liên kết với một integer key
ExampleLocalization.RegisterDynamicText(m_goExample2, ExampleLocalization.hero_name_5);
// Hoặc liên kết gameObject với một string key
ExampleLocalization.RegisterDynamicText(m_txtExample2, "hero_name_" + IDs.HERO_5);
// Hủy liên kết gameObject
ExampleLocalization.UnregisterDynamicText(m_goExample2);
```

![excel-to-unity-basic-localization-component-2](https://github.com/nbhung100914/excel-to-unity-example/assets/9100041/0fcb4c69-8816-40d7-a281-6f2f8a35db44)

### TextMeshProUGUI custom font.

Chúng ta sẽ sử dụng 3 file `characters_map_japan`, `characters_map_korean` và `characters_map_chinese` để tạo ra 3 font TextMeshPro cho 3 ngôn ngữ này. 3 file characters_map này chứa toàn bộ các ký tự xuất hiện trong sheet Localization của mỗi ngôn ngữ.

Trong ví dụ này, tôi sử dụng 3 font khác nhau để tạo ra 3 font TextMeshPro

- Nhật: NotoSerif-Bold
- Hàn: NotoSerifJP-Bold
- Trung: NotoSerifTC-Bold

Đối với mỗi font trên, hãy tạo một font TextMeshPro. Trong cửa sổ `Font Asset Creator`, tại mục `Character Set`, hãy chọn `Character From File`. Sau đó, chọn file `characters_map` tương ứng tại mục `Character File`.

![excel-to-unity-basic-font-jp](https://github.com/nbhung100914/excel-to-unity-example/assets/9100041/f56bc334-18ac-43bd-bad5-7c20eb8ffc2f)
![excel-to-unity-basic-font-kr](https://github.com/nbhung100914/excel-to-unity-example/assets/9100041/4f4cd856-f307-4155-b957-489e92a3ad35)

Với các tính năng đã trình bày, bạn đã có đủ công cụ để xây dựng một Static Database với Excel, đáp ứng đủ cho bất cứ game Casual và Mid-core nào. 
