接口列表
==================================
> notebook  
>> all - 查看用户全部笔记本  
>> get - 查看笔记本信息  
>> list - 列出笔记本下的笔记  
>> update - 修改笔记本   
>> create - 创建笔记本  
>> delete - 删除笔记本  
>
> note  
>> create - 创建笔记  
>> get - 查看笔记  
>> update - 修改笔记  
>> move - 移动笔记  
>> delete - 删除笔记  
>

数据结构
==================================

user｛
    id,
    nickname,
    notebook_list
｝

notebook｛
    id,
    [pass],
    name,
    notes_num,
    note_list
｝

note ｛
    id,
    name,
    notebook_id,
    type(0),
    data
｝

接口输入输出
==================================

notebook/all
------------------
input｛
    user-name
｝

output｛
    notebook list
｝

notebook/get
------------------
input｛
    notebook-id，
    notebook-pass
｝

output｛
    notebook-name
    notebook-notes_num
｝

notebook/list
------------------
input｛
    notebook-id，
    notebook-pass
｝

output｛
    note list
｝

notebook/update
------------------
input｛
    notebook-id，
    notebook-pass，
    new name，
    new pass
｝

output｛
    ok
｝

notebook/create
------------------
input｛
    user-name，
    notebook-name，
    notebook-pass
｝

output｛
    notebook-id
｝

notebook/delete
------------------
input｛
    notebook-id，
    notebook-pass
｝

output｛
    ok
｝

note/create
------------------
input｛
    notebook-name，
    notebook-pass
    note-name，
    note-type，
    note-data
｝

output｛
    note-id
｝

note/get
------------------
input｛
    notebook-name，
    notebook-pass
    note-id
｝

output｛
    note
｝

note/update
------------------
input｛
    notebook-name，
    notebook-pass
    note-id
    new name，
    new type，
    new data
｝

output｛
    ok
｝

note/move
------------------
input｛
    form notebook-name，
    form notebook-pass
    to notebook-name，
    to notebook-pass
    note-id
｝

output｛
    ok
｝

note/delete
------------------
input｛
    notebook-name，
    notebook-pass
    note-id
｝

output｛
    ok
｝