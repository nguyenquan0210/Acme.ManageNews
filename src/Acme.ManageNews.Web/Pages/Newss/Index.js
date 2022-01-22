$(function () {
    var l = abp.localization.getResource('ManageNews');
    var createModal = new abp.ModalManager(abp.appPath + 'Newss/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Newss/EditModal');

    var dataTable = $('#NewsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(acme.manageNews.catalog.newss.news.getList),
            columnDefs: [
                {
                    title: l('Title'),
                    data: "title"
                },
                {
                    title: l('User'),
                    data: "userName"
                },
                {
                    title: l('CategoryName'),
                    data: "categoryName"
                },
                {
                    title: l('NewsHot'),
                    data: "newsHot",
                    render: function (data) {
                        return l('Enum:Hot:' + data);
                    }
                },
                {
                    title: l('Status'),
                    data: "status",
                    render: function (data) {
                        return l('Enum:Status:' + data);
                    }
                },
                {
                    title: l('Actions'),
                    class: "text-end",
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible:
                                        abp.auth.isGranted('ManageNews.News.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible:
                                        abp.auth.isGranted('ManageNews.News.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'DeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        acme.manageNews.catalog.newss.news.delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(
                                                    l('SuccessfullyDeleted')
                                                );
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                }

            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewNewsButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});