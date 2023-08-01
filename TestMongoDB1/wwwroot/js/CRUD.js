(function ($) {
    $('#btn-add').click(function(){
        $('.flip').find('.card').addClass('flipped');
    });
    
    $('#btn-back').click(function(){
        $('.flip').find('.card').removeClass('flipped');
        resetForm();
    });

    registerButton();
})(jQuery);

function formCompleted(msg,status)
{
    if(status == 'success')
    {   
        //console.log(msg);
        if(msg.responseJSON.status == 'Ok')
        {
            if(msg.responseJSON.isUpdate)
            {

                var deptId = msg.responseJSON.deptId;
                var row = $('#tbl-depts').find('input[value="'+deptId+'"]').closest('tr');
                row.children()[1].innerText = $('#dept-name').val();   //array ni yg 1 atau 2 atau 3 ni dia kna tgk berdasarkan array table tu
                row.children()[2].innerText = $('#institution').val();
                row.children()[3].innerText = $('#addr').val();
            }
            else
            {
                var num = $('#tbl-depts > tbody').children().length + 1;
                var deptName = $('#dept-name').val();
                var institution = $('#institution').val();
                var addr = $('#addr').val();
                var token = $('input:hidden[name="__RequestVerificationToken"]').first().val();
                var colDel = ` <td>
                                    <form action="/Delete" method="post" data-ajax="true"
                                            data-ajax-method="post"
                                            data-ajax-complete="deleteCompleted">
                                        <input type="hidden" value="` + msg.responseJSON.deptId + `" name="Department.Id" />
                                        <button class="btn btn-info edit-depts" type="button"><i class="mdi mdi-pencil"></i></button>
                                        <button class="btn btn-danger"><i class="mdi mdi-delete"></i></button>
                                        <input type="hidden" name="__RequestVerificationToken" value="` + token + `"/>
                                    </form>
                                </td>`;

                $('#tbl-depts > tbody').append('<tr><td>' + num + '</td><td>' + deptName + '</td><td>' 
                                                + institution + '</td><td>' +addr+ '</td>'+ colDel +'</tr>');
            }                                
            
            registerButton();

            $('.flip').find('.card').removeClass('flipped');
        
            resetForm();
        }

        alert(msg.responseJSON.message);
    }
}

function registerButton()
{
    $('#tbl-depts').find('.btn-danger').off('click');
    $('#tbl-depts').find('.btn-danger').click(function(){
        return confirm('Are you sure?');
    });

    $('.edit-depts').off('click');
    $('.edit-depts').click(function(){
        var deptId = $(this).siblings('input:hidden[name="Department.Id"]')[0].value;
        $.getJSON('/Dept/' + deptId, function(data){
            $('#Department_Id').val(data.id);
            $('#dept-name').val(data.deptName);
            $('#institution').val(data.institution);
            $('#addr').val(data.address);
        });

        $('.flip').find('.card').addClass('flipped');
    });
}

function deleteCompleted(msg,status)
{
    if(status == 'success')
    {   
        console.log(msg);
        console.log(status);
        if(msg.responseJSON.status == 'Ok')
        {
            var deptId = msg.responseJSON.deptId
            $('#tbl-depts').find('input[value="'+deptId+'"]').closest('tr').remove();
            
            var num = 1;
            $('#tbl-depts > tbody').children().each(function(){
                $(this).children()[0].innerText = num;
                num++;
            });
        }
    }
}

function resetForm()
{
    $('#Department_Id').val('');
    $('.clear').val('');
}