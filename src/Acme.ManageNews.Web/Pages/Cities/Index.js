$(function () {
    var l = abp.localization.getResource('ManageNews');
    var createModal = new abp.ModalManager(abp.appPath + 'Cities/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Cities/EditModal');

    var dataTable = $('#CitiesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(acme.manageNews.catalog.cities.city.getList),
            columnDefs: [
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('Status'),
                    data: "status",
                    render: function (data) {
                        return l('Enum:Status:' + data);
                    }
                },
                {
                    title: l('SortOrder'),
                    data: "sortOrder"
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
                                        abp.auth.isGranted('ManageNews.Cities.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible:
                                        abp.auth.isGranted('ManageNews.Cities.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'DeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        acme.manageNews.catalog.cities.city.delete(data.record.id)
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

    $('#NewCitiesButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});