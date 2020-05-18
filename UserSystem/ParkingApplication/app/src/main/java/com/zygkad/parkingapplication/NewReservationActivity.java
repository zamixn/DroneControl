package com.zygkad.parkingapplication;

import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;

import android.os.Build;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.TimePicker;

import com.google.gson.Gson;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;
import com.google.gson.reflect.TypeToken;
import com.zygkad.parkingapplication.models.ParkingLot;

import java.io.IOException;
import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.Dictionary;
import java.util.Hashtable;
import java.util.List;

import okhttp3.Call;
import okhttp3.Callback;
import okhttp3.MediaType;
import okhttp3.MultipartBody;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;
import okhttp3.Response;
import okio.Buffer;

public class NewReservationActivity extends AppCompatActivity {

    private TimePicker timePicker;
    private Spinner parkingLotSpinner;
    private Button register_button;

    List<String> items;
    ArrayAdapter<String> adapter;
    Gson gson = new Gson();

    NewReservationActivity activity = this;

    Dictionary<String, Integer> lotIds;

    private String url = "https://78.58.170.115:3030/api";

    @RequiresApi(api = Build.VERSION_CODES.M)
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_new_reservation);

        timePicker = findViewById(R.id.timePicker);
        timePicker.setIs24HourView(true);
        timePicker.setHour(0);
        timePicker.setMinute(0);

        parkingLotSpinner = findViewById(R.id.parkingLotSpinner);
        items = new ArrayList<>();
        adapter = new ArrayAdapter<>(this, android.R.layout.simple_spinner_dropdown_item, items);
        parkingLotSpinner.setAdapter(adapter);

        register_button = findViewById(R.id.register_button);
        register_button.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                int hours = timePicker.getHour();
                int minutes = timePicker.getMinute();

                String selectedParkingLot = parkingLotSpinner.getSelectedItem().toString();

                String licensePlate = LoginActivity.LicensePlate;
                String phoneNumber = LoginActivity.PhoneNumber;

                PostParkingLotRegistration(hours, minutes, String.valueOf(lotIds.get(selectedParkingLot)), licensePlate, phoneNumber);
            }
        });
        register_button.setEnabled(false);

        GetParkingLots();
    }

    void PostParkingLotRegistration(int hours, int minutes, String selectedParkingLot, String licensePlate, String phoneNumber)
    {
        String _url = url + "/parkingLot";
        JsonObject obj = new JsonObject();
        obj.addProperty("hours",  String.valueOf(hours));
        obj.addProperty("minutes", String.valueOf(minutes));
        obj.addProperty("parkingLot", selectedParkingLot);
        obj.addProperty("licensePlate", licensePlate);
        obj.addProperty("phoneNumber", phoneNumber);

        String json = "\"" + obj.toString().replace("\"", "\\\"") + "\"";
        Log.d("PostBody - json",  json);

        RequestBody body = RequestBody.create(json, MediaType.parse("application/json"));

        final Request request = new Request.Builder().url(_url).post(body).build();

        Log.d("PostBody", bodyToString(request) );

        IndexActivity.client.newCall(request).enqueue(new Callback() {
            @Override
            public void onFailure(Call call, IOException e) {
                e.printStackTrace();
            }

            @Override
            public void onResponse(Call call, final Response response) throws IOException {
                if (!response.isSuccessful()) {
                    throw new IOException("Unexpected code " + response);
                } else {
                    Log.i("PostResult", response.body().toString());
                }
            }
        });
    }

    void GetParkingLots() {
        String _url = url + "/parkingLot";
        Request request = new Request.Builder()
                .url(_url)
                .build();

        IndexActivity.client.newCall(request).enqueue(new Callback() {
            @Override
            public void onFailure(Call call, IOException e) {
                e.printStackTrace();
            }

            @Override
            public void onResponse(Call call, final Response response) throws IOException {
                if (!response.isSuccessful()) {
                    throw new IOException("Unexpected code " + response);
                } else {
                    Type listType = new TypeToken<List<ParkingLot>>(){}.getType();
                    List<ParkingLot> lots = gson.fromJson(response.body().string(), listType);
                    lotIds = new Hashtable<>();

                    items.add("-");
                    for (ParkingLot l: lots ) {
                        items.add(l.address);
                        lotIds.put(l.address, l.id);
                        activity.runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                            adapter.notifyDataSetChanged();
                            register_button.setEnabled(true);
                            }
                        });
                        Log.d("response", l.address);
                    }
                }
            }
        });
    }

    private static String bodyToString(final Request request){

        try {
            final Request copy = request.newBuilder().build();
            final Buffer buffer = new Buffer();
            copy.body().writeTo(buffer);
            return buffer.readUtf8();
        } catch (final IOException e) {
            return "did not work";
        }
    }
}
